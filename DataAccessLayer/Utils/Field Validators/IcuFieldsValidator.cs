﻿using System;
using System.Collections.Generic;
using AlertToCareAPI.Models;
using DataModels;

namespace AlertToCareAPI.Repositories.Field_Validators
{
    public class IcuFieldsValidator
    {
        private readonly CommonFieldValidator _validator = new CommonFieldValidator();
        public void ValidateIcuRecord(Icu icu)
        {
            _validator.IsWhitespaceOrEmptyOrNull(icu.IcuId);
            _validator.IsWhitespaceOrEmptyOrNull(icu.BedsCount.ToString());
            _validator.IsWhitespaceOrEmptyOrNull(icu.LayoutId);
            
            
        }

        public void ValidateNewIcuId(string icuId, Icu icuRecord, IEnumerable<Icu> icuStore)
        {
            
            foreach (var icu in icuStore)
            {
                if (icu.IcuId == icuId)
                {
                    //ISSUE
                    throw new Exception("Invalid ICU Id");
                }
            }

            ValidateIcuRecord(icuRecord);
        }

    }
}