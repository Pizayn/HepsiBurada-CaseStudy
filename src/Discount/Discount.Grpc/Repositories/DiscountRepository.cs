﻿using Dapper;
using Discount.Grpc.Entities;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Threading.Tasks;
namespace Discount.API.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly IConfiguration _configuration;

        public DiscountRepository(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<Campaign> GetCampaign(string productCode)
        {
            using var connection = new NpgsqlConnection
                (_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var campaign = await connection.QueryFirstOrDefaultAsync<Campaign>
                ("SELECT * FROM Campaign WHERE ProductCode = @ProductCode", new { ProductCode = productCode });

            if (campaign == null)
                return new Campaign
                { ProductCode = "No Discount", TargetSalesCount = 0 };

            return campaign;
        }

        public async Task<bool> CreateCampaign(Campaign campaign)
        {
            using var connection = new NpgsqlConnection
                (_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var affected =
                await connection.ExecuteAsync
                    ("INSERT INTO Campaign (ProductCode, TargetSalesCount) VALUES (@ProductCode, @Description, @TargetSalesCount)",
                            new { ProductCode = campaign.ProductCode, TargetSalesCount = campaign.TargetSalesCount });

            if (affected == 0)
                return false;

            return true;
        }

        public async Task<bool> UpdateCampaign(Campaign campaign)
        {
            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var affected = await connection.ExecuteAsync
                    ("UPDATE Campaign SET ProductCode=@ProductCode , TargetSalesCount = @TargetSalesCount WHERE Id = @Id",
                            new { ProductCode = campaign.ProductCode, TargetSalesCount = campaign.TargetSalesCount, Id = campaign.Id });

            if (affected == 0)
                return false;

            return true;
        }

        public async Task<bool> DeleteCampaign(string productCode)
        {
            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var affected = await connection.ExecuteAsync("DELETE FROM Campaign WHERE ProductCode = @ProductCode",
                new { ProductCode = productCode });

            if (affected == 0)
                return false;

            return true;
        }
    }

}