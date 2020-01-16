﻿// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using DocumentFormat.OpenXml.Validation;
using System;

namespace DocumentFormat.OpenXml.Framework
{
    /// <summary>
    /// Describes a required item.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
    internal sealed class RequiredValidatorAttribute : VersionedValidatorAttribute
    {
        public bool IsRequired { get; set; } = true;

        protected override void ValidateVersion(ValidatorContext context)
        {
            if (IsRequired && context.Value is null)
            {
                context.CreateError(
                    description: SR.Format(ValidationResources.Sch_MissRequiredAttribute, context.QName.Name),
                    id: "Sch_MissRequiredAttribute",
                    errorType: ValidationErrorType.Schema);
            }
        }
    }
}
