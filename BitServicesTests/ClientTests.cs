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
    public class ClientTests
    {
        [TestMethod]
        public void ClientUnitTest()
        {
            Client testClient = new Client()
            {
                ClientId = 1,
                Name = "Target",
                Phone = "0282171600",
                Email = "contact@target.com.au",
                Password = "TestPassword",
                Active = true
            };
            string expectedClientName = "Target";
            string expectedClientPhone = "0282171600";
            string expectedClientEmail = "contact@target.com.au";
            string expectedClientPassword = "TestPassword";

            Assert.AreEqual(testClient.Name, expectedClientName);
            Assert.AreEqual(testClient.Phone, expectedClientPhone);
            Assert.AreEqual(testClient.Email, expectedClientEmail);
            Assert.AreEqual(testClient.Password, expectedClientPassword);
            Assert.AreEqual(testClient.Active, true);
        }
        [TestMethod]
        public void ClientManagementIntergrationTest()
        {
            ClientManagementViewModel clientManagementViewModel = new ClientManagementViewModel();
            int count = clientManagementViewModel.Clients.Count;
            Assert.AreEqual(3, count);
        }

        [TestMethod]
        public void ClientManagementObservableCollectionTest()
        {
            ObservableCollection<Client> mockClients = new ObservableCollection<Client>
            {
                new Client()
                {
                    ClientId = 1,
                    Name = "Target",
                    Phone = "0282171600",
                    Email = "contact@target.com.au",
                    Password = "TestPassword",
                    Active = true
                },
                new Client()
                {
                    ClientId = 2,
                    Name = "Woolworths",
                    Phone = "0293087367",
                    Email = "woolworths.com.au",
                    Password = "TestPassword",
                    Active = true
                },
                new Client()
                {
                    ClientId = 3,
                    Name = "JB Hi-Fi",
                    Phone = "0289186700",
                    Email = "contact@jbhifi.com",
                    Password = "TestPassword",
                    Active = true
                }
            };
            Mock<ClientManagementViewModel> mockClientManagementVM = new Mock<ClientManagementViewModel>();
            mockClientManagementVM.Setup(mc => mc.GetClients()).Returns(mockClients);
            int count = mockClientManagementVM.Object.GetClients().Count;
            Assert.AreEqual(3, count);
        }
    }
}
