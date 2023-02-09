using System.Text;
using Dapr.Client;
using Microsoft.Extensions.Configuration;
using VaultSharp;
using VaultSharp.V1.AuthMethods;
using VaultSharp.V1.AuthMethods.Token;

namespace SecretExtensions;
public static class SecretExtensions
{
    public static async Task UseVault(this IConfigurationBuilder builder, Type type, string secretPath, string secretMount, string key = "appsettings")
    {
        string environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

        try
        {
            builder.AddJsonFile($"appsettings.{environmentName}.json", false, true)
                .AddUserSecrets(type.Assembly).AddEnvironmentVariables();
        }
        catch (Exception ex)
        {
            var appsettingsName = $"appsettings.{environmentName}.json";
            throw new SecretException("An Error Occured! Appsettings or User Secrets Couldn't be Loaded. Make Sure " + appsettingsName + " is Exist. Detail :" + ex);
        }


        var configuration = builder.Build();

        if (string.IsNullOrWhiteSpace(configuration["VaultToken"]))
            throw new SecretException("VaultToken Couldn't be Null or Empty String. Provide a Valid VaultToken");
        if (string.IsNullOrWhiteSpace(configuration["VaultHost"]))
            throw new SecretException("VaultHost Couldn't be Null or Empty String. Provide a Valid VaultHost");

        try
        {
            IAuthMethodInfo authMethod = new TokenAuthMethodInfo(configuration["VaultToken"]);
            var vaultSettings = new VaultClientSettings(configuration["VaultHost"], authMethod);
            IVaultClient vaultClient = new VaultClient(vaultSettings);

            var appsettings = await vaultClient.V1.Secrets.KeyValue.V2.ReadSecretAsync(secretPath, mountPoint: secretMount);
            if (string.IsNullOrWhiteSpace(appsettings.Data?.Data?[key].ToString()))
                throw new SecretException(key + " is Not Found. Provide a Valid Vault Secret Key");
            var json = appsettings.Data.Data[key].ToString();

            if (!string.IsNullOrWhiteSpace(json))
                builder.AddJsonStream(new MemoryStream(Encoding.UTF8.GetBytes(json)));
            else
                throw new SecretException(key + " is Not Found. Provide a Valid Vault Secret Key");
        }
        catch (Exception ex)
        {
            throw new SecretException("An Error Occured At VaultSharp. Detail:" + ex);
        }
    }

    public static async Task UseVaultFromDapr(this IConfigurationBuilder builder, Type type, string secretStoreName, string secretPath, string key = "appsettings")
    {
        try
        {
            var configuration = builder.Build();

            var daprClient = new DaprClientBuilder().Build();
            var secret = await daprClient.GetSecretAsync(secretStoreName, secretPath);

            if (string.IsNullOrWhiteSpace(secret?[key]))
                throw new SecretException(key + " is Not Found. Provide a Valid Vault Secret Key");

            builder.AddJsonStream(new MemoryStream(Encoding.UTF8.GetBytes(secret[key])));
        }
        catch (Exception ex)
        {
            throw new SecretException("An Error Occured At Dapr Secret Store. Detail:" + ex);
        }

    }

    public static async Task AddVaultSecrets(this IConfigurationBuilder builder, string secretStoreName, string secretPath)
    {
        try
        {
            var configuration = builder.Build();

            var daprClient = new DaprClientBuilder().Build();
            var secret = await daprClient.GetSecretAsync(secretStoreName, secretPath);

            builder.AddInMemoryCollection(secret);
        }
        catch (Exception ex)
        {
            throw new SecretException("An Error Occured At Dapr Secret Store. Detail:" + ex);
        }
    }

    public class SecretException : Exception
    {
        public SecretException(string message) : base(message) { }
    }
}