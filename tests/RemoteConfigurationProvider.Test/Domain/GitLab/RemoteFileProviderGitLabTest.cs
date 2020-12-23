using System;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using RemoteConfigurationProvider.Domain;
using RemoteConfigurationProvider.Domain.GitLab;
using Xunit;

namespace RemoteConfigurationProvider.Test.Domain.GitLab
{
    public class RemoteFileProviderGitLabTest : RemoteFileProviderTest
    {
        [Fact]
        public void Should_Validate_Configured_Remote_File_From_Private_Repository()
        {
            // arrange
            var remoteFileProvider = new RemoteFileProviderGitLab("https://gitlab.com/api/v4/projects/23263506/repository/files/master.json?ref=master", "Dw7H_EAxXQYHwzV-dvyc");

            // act
            var configuration = new ConfigurationBuilder()
                .AddRemoteJsonFile(remoteFileProvider)
                .Build();

            // assert
            var resultExpected = ResultExpected();
            resultExpected["RESULT:REMOTE_APPSETTINGS"] = (typeof(string), "Private");
            foreach (var (path, result) in resultExpected)
                configuration.GetSection(path).Get(result.type).Should().BeEquivalentTo(result.resultExpected);
        }
        
        [Fact]
        public void Should_Validate_Configured_Remote_File_From_Public_Repository()
        {
            // arrange
            var remoteFileProvider = new RemoteFileProviderGitLab("https://gitlab.com/api/v4/projects/23263490/repository/files/master.json?ref=master");

            // act
            var configuration = new ConfigurationBuilder()
                .AddRemoteJsonFile(remoteFileProvider)
                .Build();

            // assert
            var resultExpected = ResultExpected();
            resultExpected["RESULT:REMOTE_APPSETTINGS"] = (typeof(string), "Public");
            foreach (var (path, result) in resultExpected)
                configuration.GetSection(path).Get(result.type).Should().BeEquivalentTo(result.resultExpected);
        }

        [Fact]
        public void Should_Thrown_Exception_When_Acess_Token_Invalid()
        {
            // arrange
            var remoteFileProvider = new RemoteFileProviderGitLab("https://gitlab.com/api/v4/projects/23263506/repository/files/master.json?ref=master", "invalid-accesstoken");

            // act
            var exception = new ConfigurationBuilder()
                .Invoking(lnq => lnq.AddRemoteJsonFile(remoteFileProvider)
                    .Build())
                .Should()
                .Throw<Exception>();

            // assert
            exception.And.Message.Should().Contain("401 Unauthorized");
        }
        
         [Fact]
        public void Should_Thrown_Exception_When_File_Not_Found()
        {
            // arrange
            var remoteFileProvider = new RemoteFileProviderGitLab("https://gitlab.com/api/v4/projects/23263490/repository/files/masters.json?ref=master");

            // act
            var exception = new ConfigurationBuilder()
                .Invoking(lnq => lnq.AddRemoteJsonFile(remoteFileProvider)
                    .Build())
                .Should()
                .Throw<Exception>();

            // assert
            exception.And.Message.Should().Contain("404 File Not Found");
        }
    }
}