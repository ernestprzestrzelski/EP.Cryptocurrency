Application consists of two main components:

EP.Cryptocurrency.Web - a front for viewing currenct cryptocurrency listings
EP.Cryptocurrency.Synchronizer - a tool for downloading current cryptocurrency listings from CoinMarketCap API

Setup:
1. EP.Cryptocurrency.Web
a) Configure connection string to the database in the appSettings.json file
b) Run Migrations in the EP.Cryptocurrency.Storage project against the database - this should create the database

2. EP.Cryptocurrency.Synchronizer
a) Configure connection string to the database in the appSettings.json file
b) Set CoinMarketCapApi base address to your liking (https://sandbox-api.coinmarketcap.com/ - developer testing environment, https://pro-api.coinmarketcap.com/ - production live environment) in the appSettings.json file
c) Set your API Key in the appSettings.json file. To obtain your API key, you need to sign up to CoinMarketCap.

To start the application you just need to run EP.Cryptocurrency.Web and synchronize data in the database by running EP.Cryptocurrency.Synchronizer.