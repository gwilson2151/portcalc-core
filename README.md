### Summary
This is a personal project that automates investment portfolio weight calculations that I usually have to do by hand. As with many personal projects, there's never enough time to work on it. 🙂 As a result tests are inadequate, corners have been cut, and it doesn't do nearly as much as I want it to.

The functionality in `[S]how Accounts` should work for all accounts and `Calculate [W]eights` works only if you have the exact ETFs and/or stocks set up in HardCodedLoader.cs

The functionality in `[L]oad Portfolio Definition` is the beginning of an experiment continued in another branch (😐 and not finished and I'm not happy with its state so please ignore it).

### Functionality
The console application will use the Questrade API to retrieve all of your investment accounts and the positions and cash balances within and display a summary of it all.

#### Retrieval
The code in the `Questrade` library controls retrieving information from the Questrade API. A refresh token is required to communicate with the Questrade API so there is some session state that needs to be tracked when communicating with the API. When making an API call, if the token in the session expires then a new refresh token will be retrieved from the API before making the call to ensure success.

#### State
The code in the `Contract` library stores the state of the accounts, positions, cash balances, etc.

#### Summary Report
The code in the `PortfolioService.cs` class is responsible for taking the state of the accounts, positions, and cash balances and printing the summary in the console. It's very imperative and I'd like to build reports that are more generic in the future.

### Usage
To use the application you'll need a Questrade account. Assuming you have one, you'll need to have a refresh token from the [App Hub](https://apphub.questrade.com/UI/UserApps.aspx) (or on the website click the user dropdown on the top right of the screen > click App Hub).

#### Visual Studio 2019
1. Set PortfolioApp as the start up project.
2. Build.
3. Run.

#### CLI
1. `cd` to folder containing `portcalc.sln`.
2. `dotnet build`
3. `dotnet run --project src/PortfolioApp/PortfolioApp.csproj`

#### Once running
1. Select `s` or `w`.
2. Paste refresh token if the first time using the Questrade API since starting the application.
