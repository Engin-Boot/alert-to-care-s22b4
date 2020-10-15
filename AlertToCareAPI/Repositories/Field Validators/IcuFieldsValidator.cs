﻿using System;
using System.Collections.Generic;
using AlertToCareAPI.Models;

namespace AlertToCareAPI.Repositories.Field_Validators
{
    public class IcuFieldsValidator
    {
        private readonly CommonFieldValidator _validator = new CommonFieldValidator();
        private readonly PatientFieldsValidator _patientRecordValidator = new PatientFieldsValidator();
        public void ValidateIcuRecord(Icu icu)
        {
            _validator.IsWhitespaceOrEmptyOrNull(icu.IcuId);
            _validator.IsWhitespaceOrEmptyOrNull(icu.BedsCount.ToString());
            _validator.IsWhitespaceOrEmptyOrNull(icu.LayoutId);
            
            
        }

        public void ValidateOldIcuId(string icuId, List<Icu> icuStore)
        {
            foreach (var icu in icuStore)
            {
                if (icu.IcuId == icuId)
                {
                    return;
                }
            }
            throw new Exception("Invalid Patient Id");
        }

        public void ValidateNewIcuId(string icuId, Icu icuRecord, List<Icu> icuStore)
        {
            
            foreach (var icu in icuStore)
            {
                if (icu.IcuId == icuId)
                {
                    throw new Exception("Invalid Patient Id");
                }
            }

            ValidateIcuRecord(icuRecord);
        }

    }
}
