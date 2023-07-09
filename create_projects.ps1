if($true -eq (Test-Path .\src))
{
    throw "src folder already exists"
}
else {

    if($true -eq (Test-Path .\tests))
    {
        throw "tests folder already exists"
    }
    else {


        $null = mkdir .\tests\UseCases
        dotnet new xunit -n interview.test.ensek.Tests.UseCases -o .\tests\UseCases

        $null = mkdir .\src
        $null = mkdir .\src\Core\Contracts
        $null = mkdir .\src\Core\Domain
        $null = mkdir .\src\Core\Services\Abstractions
        $null = mkdir .\src\Host\WebApi
        $null = mkdir .\src\Infrastructure\Services
        $null = mkdir .\src\Infrastructure\SqlServer

        dotnet new classlib -n interview.test.ensek.Core.Contracts -o .\src\Core\Contracts
        dotnet new classlib -n interview.test.ensek.Core.Domain -o .\src\Core\Domain
        dotnet new classlib -n interview.test.ensek.Core.Services -o .\src\Core\Services
        dotnet new classlib -n interview.test.ensek.Core.Services.Abstractions -o .\src\Core\Services.Abstractions
        dotnet new webapi -n interview.test.ensek.Host.WebApi -o .\src\Host\WebApi
        dotnet new classlib -n interview.test.ensek.Infrastructure.Services -o .\src\Infrastructure\Services
        dotnet new classlib -n interview.test.ensek.Infrastructure.SqlServer -o .\src\Infrastructure\SqlServer

        dotnet new sln -n interview.test.ensek
        ls *.csproj -Recurse | ? { dotnet sln add $_.FullName } 

        ls class1.cs -Recurse | ? { rm $_ -force}

    }
}
