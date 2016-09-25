﻿#region copyright
// Copyright 2016 Digital Receipt Exchange Limited
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
// http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// 
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Net.Dreceiptx.Users;
using NUnit.Framework;

namespace Net.Dreceiptx.UnitTests.Users
{
    [TestFixture]
    public class EnumExtensionsTest
    {
        [Test]
        public void TestEnumAttribute()
        {
            Assert.AreEqual("ENDPOINTID", UserConfigOptionType.EndPointId.Value());
        }

        public enum TestEnum
        {
            [DrxEnumExtendedInformationAttribute("ONE", "ONEDESCRIPTION")]
            One,
            [DrxEnumExtendedInformationAttribute("TWO", "TWODESCRIPTION")]
            Two,
            [DrxEnumExtendedInformationAttribute("THREE", "THREEDESCRIPTION")]
            Three
        }
    }
}