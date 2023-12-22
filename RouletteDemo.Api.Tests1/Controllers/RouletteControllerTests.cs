using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using RouletteDemo.Api.Controllers;
using RouletteDemo.Api.Dtos;
using RouletteDemo.Api.Models;
using RouletteDemo.Api.Services;

namespace RouletteDemo.Api.Tests1.Controllers
{
    public class RouletteControllerTests
    {
        private IRouletteService _rouletteService;
        private RouletteController _rouletteController;

        [SetUp]
        public void Setup()
        {
            _rouletteService = Substitute.For<IRouletteService>();
            _rouletteController = new RouletteController(_rouletteService);                
        }

        [Test]
        public async Task RouletteController_PlaceBet_ReturnOK()
        {
            var placeBetRequest = Substitute.For<BetDto>();
            var response = await _rouletteController.PlaceBet(placeBetRequest);
            Assert.IsNotNull(response);
        }

        [Test]
        public async Task RouletteController_Spin_ReturnOK()
        {
            var response = await _rouletteController.Spin();
            Assert.IsNotNull(response);
        }

        [Test]
        public async Task RouletteController_Payout_ReturnOK()
        {
            var response = await _rouletteController.Payout();
            Assert.IsNotNull(response);
        }

        [Test]
        public async Task RouletteController_ShowPreviousSpins_ReturnOK()
        {
            var response = await _rouletteController.ShowPreviousSpins();
            Assert.IsNotNull(response);
        }
    }


}