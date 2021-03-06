﻿//-----------------------------------------------------------------------
// <copyright file="SetShippingMethodsInputModel.cs" company="Sitecore Corporation">
//     Copyright (c) Sitecore Corporation 1999-2016
// </copyright>
// <summary>Controller parameters required to set the shipping methods into the cart.</summary>
//-----------------------------------------------------------------------
// Copyright 2016 Sitecore Corporation A/S
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file 
// except in compliance with the License. You may obtain a copy of the License at
//       http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software distributed under the 
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, 
// either express or implied. See the License for the specific language governing permissions 
// and limitations under the License.
// -------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sitecore.Foundation.Commerce.Models.InputModels
{
    public class SetShippingMethodsInputModel
    {
        [Required]
        public string OrderShippingPreferenceType { get; set; }

        [Required]
        public List<ShippingMethodInputModelItem> ShippingMethods { get; set; } =
            new List<ShippingMethodInputModelItem>();

        public List<PartyInputModelItem> ShippingAddresses { get; set; } =
            new List<PartyInputModelItem>();
    }
}