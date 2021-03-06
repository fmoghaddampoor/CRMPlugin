﻿namespace MyFirstPluginCollection.Query
{
    public interface ITargetQueryable
    {
        EntityResult SearchTable(string tableName, string FieldLogicalValue, string FieldValue);
        UpdateCellResult UpdateCell(string tableName, int RecordId, string fieldLogicalValue, string fieldValue);
        EntityResult GetPostImage();
        EntityResult GetPreImage();
    }
}
