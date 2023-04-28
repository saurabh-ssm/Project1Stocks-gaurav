using Project1Stocks.Data;
using Hangfire;
using System;


namespace Project1Stocks.Jobs
{
    public class StockUpdater
    {
         
        private readonly ApplicationDbContext _dbContext;

        public StockUpdater(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void UpdateWipro()
        {
            var wipro = _dbContext.Companies.Single(c => c.Name == "Wipro");
            // update wipro's stock price in the database
            
            Random rand = new Random();
            int randnum = rand.Next(200, 500);

            // update wipro's stock price in the database
            wipro.StockPrice = randnum;
            //wipro.StockPrice =x+1;
            _dbContext.SaveChanges();
        }

        public void UpdateZensar()
        {
            var zensar = _dbContext.Companies.Single(c => c.Name == "Zensar");
            // update zensar's stock price in the database
            Random rand = new Random();
            int randNum = rand.Next(200, 500);

            // update wipro's stock price in the database
            zensar.StockPrice = randNum;
            _dbContext.SaveChanges();
        }

        public void UpdateTCS()
        {
            var tcs = _dbContext.Companies.Single(c => c.Name == "TCS");
            // update tcs's stock price in the database
            Random rand = new Random();
            int randNum = rand.Next(200, 500);

            // update wipro's stock price in the database
            tcs.StockPrice = randNum;

            _dbContext.SaveChanges();
        }

        public void UpdateTesla()
        {
            var tesla = _dbContext.Companies.Single(c => c.Name == "Tesla");
            Random rand = new Random();
            int randNum = rand.Next(200, 500);

            // update wipro's stock price in the database
            tesla.StockPrice = randNum;
            // update tesla's stock price in the database

            _dbContext.SaveChanges();
        }

        public void UpdateApple()
        {
            var apple = _dbContext.Companies.Single(c => c.Name == "Apple");
            // update apple's stock price in the database
            Random rand = new Random();
            int randNum = rand.Next(200, 500);

            // update wipro's stock price in the database
            apple.StockPrice = randNum;

            _dbContext.SaveChanges();
        }
    }


}
