﻿using System;
using NUnit.Framework;
using TAlex.BeautifulFractals.ViewModels;
using TAlex.Common.Environment;
using TAlex.Common.Licensing;
using NSubstitute;
using FluentAssertions;
using TAlex.BeautifulFractals.Services.Windows;


namespace TAlex.BeautifulFractals.Test.ViewModels
{
    [TestFixture]
    public class AboutViewModelTests
    {
        private AboutViewModel ViewModel;
        private ApplicationInfo ApplicationInfoMock;
        private LicenseBase LicenseBaseMock;
        private IRegistrationWindowService RegistrationWindowServiceMock;


        [SetUp]
        public void SetUp()
        {
            ApplicationInfoMock = Substitute.For<ApplicationInfo>();
            LicenseBaseMock = Substitute.For<LicenseBase>(Substitute.For<ILicenseDataManager>(), Substitute.For<ITrialPeriodDataProvider>());
            RegistrationWindowServiceMock = Substitute.For<IRegistrationWindowService>();

            ViewModel = new AboutViewModel(ApplicationInfoMock, LicenseBaseMock, RegistrationWindowServiceMock);
        }


        [TestCase("Title")]
        [TestCase("Beautiful Fractals")]
        public void AboutLogoTitle_Test(string expected)
        {
            //arrange
            ApplicationInfoMock.Title.Returns(expected);

            //action
            string actual = ViewModel.AboutLogoTitle;

            //assert
            actual.Should().Be(expected);
        }

        [TestCase(1, 0, 0, 5)]
        public void Version_Test(int major, int minor, int build, int revision)
        {
            //arrange
            var expected = new Version(major, minor, build, revision);
            ApplicationInfoMock.Version.Returns(expected);

            //action
            Version actual = ViewModel.Version;

            //assert
            actual.Should().Be(expected);
        }

        [Test]
        public void EmailAddress_Test()
        {
            //action
            string actual = ViewModel.EmailAddress; 

            //assert
            actual.Should().Be(TAlex.BeautifulFractals.Properties.Resources.SupportEmail);
        }

        [Test]
        public void HomepageUrl_Test()
        {
            //action
            string actual = ViewModel.HomepageUrl;

            //assert
            actual.Should().Be(TAlex.BeautifulFractals.Properties.Resources.HomepageUrl);
        }

        [TestCase("Some Copyright")]
        public void Copyright_Test(string expected)
        {
            //arrange
            ApplicationInfoMock.CopyrightDisplayText.Returns(expected);

            //action
            string actual = ViewModel.Copyright;

            //assert
            actual.Should().Be(expected);
        }

        [TestCase("License Name")]
        public void LicenseName_Test(string expected)
        {
            //arrange
            LicenseBaseMock.LicenseName.Returns(expected);

            //action
            string actual = ViewModel.LicenseName;

            //assert
            actual.Should().Be(expected);
        }

        [Test]
        public void LicenseInfoVisibility_Test([Values(true, false)]bool expected)
        {
            //arrange
            LicenseBaseMock.IsLicensed.Returns(expected);

            //action
            bool actual = ViewModel.LicenseInfoVisibility;

            //assert
            actual.Should().Be(expected);
        }

        [Test]
        public void UnregisteredTextVisibility_Test([Values(true, false)]bool isLicensed)
        {
            //arrange
            var expected = !isLicensed;
            LicenseBaseMock.IsLicensed.Returns(isLicensed);

            //action
            bool actual = ViewModel.UnregisteredTextVisibility;

            //assert
            actual.Should().Be(expected);
        }
    }
}