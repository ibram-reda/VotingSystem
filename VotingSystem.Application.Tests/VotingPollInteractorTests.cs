﻿using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VotingSystem.core;
using VotingSystem.core.Models;
using VotingSystem.Database;
using Xunit;

namespace VotingSystem.Application.Tests
{
    public class VotingPollInteractorTests
    {
        private readonly VotingPollCreationRequest _request = new VotingPollCreationRequest();
        private readonly Mock<IVotingPollFactory> _mockFactory = new Mock<IVotingPollFactory>();
        private readonly Mock<IVotingSystemPresistance> _mockPresistance = new Mock<IVotingSystemPresistance>();
        private  VotingPollInteractor _interactor;
        public VotingPollInteractorTests()
        {
            _interactor = new VotingPollInteractor(_mockFactory.Object, _mockPresistance.Object); ;
        }
        [Fact]
        public async Task CreateVotingPoll_UsesVotingPollFactoryToCreateVotingPoll()
        {
            await _interactor.CreateVotingPollAsync(_request);

            _mockFactory.Verify(x => x.CreatePoll(_request));
        }

        [Fact]
        public async Task CreateVotingPoll_PersistsCreatedPoll()
        {
            var poll = new VotingPoll();
            _mockFactory.Setup(x => x.CreatePoll(_request)).Returns(poll);
            
            await _interactor.CreateVotingPollAsync(_request);

            _mockPresistance.Verify(x => x.SaveVotingPollAsync(poll));
        }

    }
}
