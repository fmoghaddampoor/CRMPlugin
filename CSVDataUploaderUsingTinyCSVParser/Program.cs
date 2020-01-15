using System.Configuration;
using System.ServiceModel.Description;
using System.Net;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Query;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using System.Threading.Tasks;
using TinyCsvParser;
using CSVDataUploader;

namespace CSVDataUploaderAdvanced
{
    class Program
    {
        static void Main(string[] args)
        {
            var organizationServiceProxy = new OrganizationServiceProxy(GetUri(), null, GetClientCredentials(), null);
            DataTable csvDataTable = GetCSVData();
            ExecuteMultipleRequest executeMultipleRequest1 = new ExecuteMultipleRequest()
            {
                Settings = new ExecuteMultipleSettings
                {
                    ContinueOnError = true,
                    ReturnResponses = true
                }
                ,
                Requests = new OrganizationRequestCollection()
            };
            ExecuteMultipleRequest executeMultipleRequest2 = new ExecuteMultipleRequest()
            {
                Settings = new ExecuteMultipleSettings
                {
                    ContinueOnError = true,
                    ReturnResponses = true
                }
                ,
                Requests = new OrganizationRequestCollection()
            };
            int counter = 0;
            for (int locIndexRow=1; locIndexRow< csvDataTable.Rows.Count; locIndexRow++)
            {
                string name = csvDataTable.Rows[locIndexRow][0].ToString();
                string primaryContact = csvDataTable.Rows[locIndexRow][1].ToString();
                string telephone1 = csvDataTable.Rows[locIndexRow][2].ToString();
                QueryExpression queryExpression = new QueryExpression("contact");
                queryExpression.ColumnSet = new ColumnSet(false);
                queryExpression.NoLock = true;
                queryExpression.TopCount = 1;
                queryExpression.Criteria.AddCondition("fullname", ConditionOperator.Equal, primaryContact);
                Entity queryResult = organizationServiceProxy.RetrieveMultiple(queryExpression).Entities.FirstOrDefault();
                if (queryResult!=null)
                {
                    EntityReference primaryContactReference = queryResult.ToEntityReference();
                    Entity customEntity = CreateCustomEntity("account", name, primaryContactReference, telephone1);
                    UpsertRequest upsertRequest = new UpsertRequest()
                    {
                        Target = customEntity
                    };
                    if(locIndexRow % 2 == 0)
                    {
                        executeMultipleRequest1.Requests.Add(upsertRequest);
                    }
                    else
                    {
                        executeMultipleRequest2.Requests.Add(upsertRequest);
                    }
                    counter++;
                    Console.WriteLine($"Request {counter} added");
                }
            }
            Console.WriteLine("Started executing request.");
            Task.WaitAll(Task.Run(()=> 
            {
                ExecuteMultipleResponse executeMultipleResponse = (ExecuteMultipleResponse)organizationServiceProxy.Execute(executeMultipleRequest1); 
                foreach(var response in executeMultipleResponse.Responses)
                {
                    if(response.Fault!=null)
                    {
                        Console.WriteLine(response.Fault.Message);
                    }
                    else
                    {
                        Console.WriteLine("Request1 executed done!");
                    }
                }
   
            }),
            Task.Run(() =>
            {
                ExecuteMultipleResponse executeMultipleResponse = (ExecuteMultipleResponse)organizationServiceProxy.Execute(executeMultipleRequest2);
                foreach (var response in executeMultipleResponse.Responses)
                {
                    if (response.Fault != null)
                    {
                        Console.WriteLine(response.Fault.Message);
                    }
                    else
                    {
                        Console.WriteLine("Request2 executed done!");
                    }
                }

            }));
            executeMultipleRequest1.Requests.Clear();
            executeMultipleRequest2.Requests.Clear();
            Console.WriteLine("Requests have been executed");
            Console.ReadLine();
        }

        private static Entity CreateCustomEntity(string tableName, string name, EntityReference primaryContact , string telephone1)
        {
            return new Entity(tableName)
            {
                ["name"] = name,
                ["primarycontactid"] = primaryContact,
                ["telephone1"] = telephone1
            };
        }

        private static ClientCredentials GetClientCredentials()
        {
            ClientCredentials clientCredentials = new ClientCredentials();
            clientCredentials.UserName.UserName = ConfigurationManager.ConnectionStrings["user"].ToString();
            clientCredentials.UserName.Password = ConfigurationManager.ConnectionStrings["password"].ToString();
            return clientCredentials;
        }

        private static SecurityProtocolType GetSecurityProtocolType()
        {
            return SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
        }

        private static System.Uri GetUri()
        {
            return new System.Uri(ConfigurationManager.ConnectionStrings["url"].ToString());
        }

        private static string GetFilePath()
        {
            return ConfigurationManager.ConnectionStrings["ExcelCSVPath"].ToString();
        }

        private static DataTable GetCSVData()
        {
            CsvParserOptions csvParserOptions = new CsvParserOptions(false, ',');
            CsvPersonMapping csvMapper = new CsvPersonMapping();
            CsvParser<Account> csvParser = new CsvParser<Account>(csvParserOptions, csvMapper);

            var csvResult = csvParser
                .ReadFromFile(GetFilePath(), Encoding.UTF8)
                .ToList();

            DataTable dt = null;
            if (csvResult.Count > 0)
            {
                dt = new DataTable();
                dt.Columns.Add("Name");
                dt.Columns.Add("PrimaryContactId");
                dt.Columns.Add("Telephone1");
                for (int locIndexLine = 0; locIndexLine < csvResult.Count; locIndexLine++)
                {
                    dt.Rows.Add();
                    dt.Rows[locIndexLine]["Name"] = csvResult[locIndexLine].Result.Name;
                    dt.Rows[locIndexLine]["PrimaryContactId"] = csvResult[locIndexLine].Result.PrimaryContactId;
                    dt.Rows[locIndexLine]["Telephone1"] = csvResult[locIndexLine].Result.Telephone1;
                }
            }
            return dt;
        }

        private static string[] ReadFileLines(string fileName)
        {
            List<string> lstLines = new List<string>();
            using (System.IO.FileStream fileStream = new System.IO.FileStream(fileName, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read))
            {
                using (System.IO.StreamReader sr = new System.IO.StreamReader(fileStream,Encoding.UTF8))
                {
                    string line = String.Empty;
                    while ((line = sr.ReadLine()) != null)
                    {
                        lstLines.Add(line);
                    }
                }
            }
            return lstLines.ToArray();
        }

        private static char GetFileDelimiter(string input)
        {
            List<char> delimiters = new List<char> { ' ', ';', '-' , ','};
            Dictionary<char, int> dicCharCount = delimiters.ToDictionary(key => key, value => 0);
            foreach (char delimiter in delimiters)
            {
                dicCharCount[delimiter] = input.Count(t => t == delimiter);
            }
            return dicCharCount.OrderByDescending(x => x.Value).First().Key;
        }
    }
}
