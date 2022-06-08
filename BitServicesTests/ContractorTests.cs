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
    public class ContractorTests
    {
        [TestMethod]
        public void ContractorUnitTest()
        {
            Contractor testContractor = new Contractor()
            {
                ContractorId = 1,
                FirstName = "Bessie",
                LastName = "Camacho",
                Phone = "0455520384",
                Email = "bessie.camacho@bitservices.com",
                Street = "80 Druitt St",
                Suburb = "Sydney",
                Postcode = "2000",
                State = "NSW",
                LicenceNumber = "9387212549",
                VehicleRegistration = "CB33GO",
                PerformanceRating = 8,
                Password = "TestPassword",
                Active = true
            };
            string expectedContractorFirstName = "Bessie";
            string expectedContractorLastName = "Camacho";
            string expectedClientPhone = "0455520384";
            string expectedClientEmail = "bessie.camacho@bitservices.com";
            string expectedContractorStreet = "80 Druitt St";
            string expectedContractorSuburb = "Sydney";
            string expectedContractorPostcode = "2000";
            string expectedContractorState = "NSW";
            string expectedContractorLicenceNumber = "9387212549";
            string expectedContractorVehicleRegistration = "CB33GO";
            int expectedContractorPerformanceRating = 8;
            string expectedClientPassword = "TestPassword";

            Assert.AreEqual(testContractor.FirstName, expectedContractorFirstName);
            Assert.AreEqual(testContractor.LastName, expectedContractorLastName);
            Assert.AreEqual(testContractor.Phone, expectedClientPhone);
            Assert.AreEqual(testContractor.Email, expectedClientEmail);
            Assert.AreEqual(testContractor.Street, expectedContractorStreet);
            Assert.AreEqual(testContractor.Suburb, expectedContractorSuburb);
            Assert.AreEqual(testContractor.Postcode, expectedContractorPostcode);
            Assert.AreEqual(testContractor.State, expectedContractorState);
            Assert.AreEqual(testContractor.LicenceNumber, expectedContractorLicenceNumber);
            Assert.AreEqual(testContractor.VehicleRegistration, expectedContractorVehicleRegistration);
            Assert.AreEqual(testContractor.PerformanceRating, expectedContractorPerformanceRating);
            Assert.AreEqual(testContractor.Password, expectedClientPassword);
            Assert.AreEqual(testContractor.Active, true);
        }
        [TestMethod]
        public void ContractorManagementIntergrationTest()
        {
            ContractorManagementViewModel clientManagementViewModel = new ContractorManagementViewModel();
            int count = clientManagementViewModel.Contractors.Count;
            Assert.AreEqual(6, count);
        }
            
        [TestMethod]
        public void ContractorManagementMoqTest()
        {
            ObservableCollection<Contractor> mockContractors = new ObservableCollection<Contractor>
            {
                new Contractor()
                {
                    ContractorId = 1,
                    FirstName = "Bessie",
                    LastName = "Camacho",
                    Phone = "0455520384",
                    Email = "bessie.camacho@bitservices.com",
                    Street = "80 Druitt St",
                    Suburb = "Sydney",
                    Postcode = "2000",
                    State = "NSW",
                    LicenceNumber = "9387212549",
                    VehicleRegistration = "CB33GO",
                    PerformanceRating = 8,
                    Password = "TestPassword",
                    Active = true
                },
                new Contractor()
                {
                    ContractorId = 2,
                    FirstName = "Valarie",
                    LastName = "Palmer",
                    Phone = "0455546672",
                    Email = "valarie.palmer@bitservices.com",
                    Street = "6/155 King St",
                    Suburb = "Sydney",
                    Postcode = "2000",
                    State = "NSW",
                    LicenceNumber = "7899293455",
                    VehicleRegistration = "PSY845",
                    PerformanceRating = 7,
                    Password = "TestPassword",
                    Active = true
                },
                new Contractor()
                {
                    ContractorId = 3,
                    FirstName = "Barrett",
                    LastName = "Snyder",
                    Phone = "0455578787",
                    Email = "barrett.snyder@bitservices.com",
                    Street = "397 George St",
                    Suburb = "Sydney",
                    Postcode = "2000",
                    State = "NSW",
                    LicenceNumber = "0062351808",
                    VehicleRegistration = "AA56QH",
                    PerformanceRating = 8,
                    Password = "TestPassword",
                    Active = true
                }
            };
            Mock<ContractorManagementViewModel> mockContractorManagementVM = new Mock<ContractorManagementViewModel>();
            mockContractorManagementVM.Setup(mc => mc.GetContractors()).Returns(mockContractors);
            int count = mockContractorManagementVM.Object.GetContractors().Count;
            Assert.AreEqual(3, count);
        }
    }
}
