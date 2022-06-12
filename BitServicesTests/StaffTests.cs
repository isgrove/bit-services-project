using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.ObjectModel;
using BitServicesDesktopApp.Models;
using BitServicesDesktopApp.ViewModels;
using BitServicesDesktopApp.Views;
using Moq;

namespace BitServicesTests
{

    [TestClass]
    public class StaffTests
    {
        [TestMethod]
        public void StaffUnitTest()
        {
            Staff testStaff = new Staff()
            {
                StaffId = 1,
                FirstName = "Darla",
                LastName = "Proctor",
                Phone = "0455573663",
                Email = "darla.proctor@bitservices.com",
                Password = "TestPassword",
                StaffType = "Coordinator",
                Active = true
            };
            int expectedStaffId = 1;
            string expectedStaffFirstName = "Darla";
            string expectedStaffLastName = "Proctor";
            string expectedStaffPhone = "0455573663";
            string expectedStaffEmail = "darla.proctor@bitservices.com";
            string expectedStaffType = "Coordinator";
            string expectedStaffPassword = "TestPassword";

            Assert.AreEqual(testStaff.StaffId, expectedStaffId);
            Assert.AreEqual(testStaff.FirstName, expectedStaffFirstName);
            Assert.AreEqual(testStaff.LastName, expectedStaffLastName);
            Assert.AreEqual(testStaff.Phone, expectedStaffPhone);
            Assert.AreEqual(testStaff.Email, expectedStaffEmail);
            Assert.AreEqual(testStaff.StaffType, expectedStaffType);
            Assert.AreEqual(testStaff.Password, expectedStaffPassword);
            Assert.AreEqual(testStaff.Active, true);
        }

        [TestMethod]
        public void StaffManagementIntergrationTest()
        {
            StaffManagementViewModel staffManagementViewModel = new StaffManagementViewModel();
            int count = staffManagementViewModel.Staffs.Count;
            Assert.AreEqual(6, count);
        }

        [TestMethod]
        public void StaffManagementMoqTest()
        {
            ObservableCollection<Staff> mockStaff = new ObservableCollection<Staff>
            {
                new Staff()
                {
                    StaffId = 1,
                    FirstName = "Darla",
                    LastName = "Proctor",
                    Phone = "0455573663",
                    Email = "darla.proctor@bitservices.com",
                    Password = "TestPassword",
                    StaffType = "Coordinator",
                    Active = true
                },
                new Staff()
                {
                    StaffId = 2,
                    FirstName = "Douglas",
                    LastName = "Finley",
                    Phone = "0455511305",
                    Email = "douglas.finley@bitservices.com",
                    Password = "TestPassword",
                    StaffType = "Coordinator",
                    Active = true
                },
                new Staff()
                {
                    StaffId = 3,
                    FirstName = "Ana",
                    LastName = "Stevenson",
                    Phone = "0455596376",
                    Email = "ana.stevenson@bitservices.com",
                    Password = "TestPassword",
                    StaffType = "Coordinator",
                    Active = true
                }
            };
            Mock<StaffManagementViewModel> mockStaffManagementVM = new Mock<StaffManagementViewModel>();
            mockStaffManagementVM.Setup(mc => mc.GetStaff()).Returns(mockStaff);
            int count = mockStaffManagementVM.Object.GetStaff().Count;
            Assert.AreEqual(3, count);
        }
    }
}
