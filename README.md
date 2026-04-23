# asp-net-core-ci-cd

Example project to demonstarte CI/CD with an ASP.NET Core web app.

## Azure deployment (Bicep + GitHub OIDC)

The [`infra/main.bicep`](infra/main.bicep) template provisions a Linux App Service on the Free (F1) tier for running the app. Deployment from GitHub Actions uses OIDC federated credentials — no publish profile or client secret.

### One-time Azure setup

Do this once per target subscription. You need `az` logged in as someone with rights to create app registrations and role assignments.

1. **Create the resource group.**

   ```sh
   az group create -n rg-aspnet-core-test -l westeurope
   ```

2. **Create an app registration and service principal.**

   ```sh
   az ad app create --display-name gh-aspnet-core-ci-cd
   # copy the appId from the output → this is AZURE_CLIENT_ID
   az ad sp create --id <appId>
   ```

3. **Add a federated credential trusting this repo's `main` branch.**
   Save this as `fic.json` (replace `<owner>` with your GitHub username/org):

   ```json
   {
     "name": "github-main",
     "issuer": "https://token.actions.githubusercontent.com",
     "subject": "repo:<owner>/asp-net-core-ci-cd:ref:refs/heads/main",
     "audiences": ["api://AzureADTokenExchange"]
   }
   ```

   Then:

   ```sh
   az ad app federated-credential create --id <appId> --parameters @fic.json
   ```

4. **Grant the SP Contributor on the resource group.**

   ```sh
   az role assignment create \
     --assignee <appId> \
     --role Contributor \
     --scope /subscriptions/<sub-id>/resourceGroups/rg-aspnet-core-test
   ```

5. **Add three GitHub repo secrets** (Settings → Secrets and variables → Actions):
   - `AZURE_CLIENT_ID` — the app registration's appId
   - `AZURE_TENANT_ID` — `az account show --query tenantId -o tsv`
   - `AZURE_SUBSCRIPTION_ID` — `az account show --query id -o tsv`

### Deploy the infra manually (first time)

```sh
az deployment group create \
  --resource-group rg-aspnet-core-test \
  --template-file infra/main.bicep \
  --parameters infra/main.parameters.example.json
```

Copy `main.parameters.example.json` to `main.parameters.json` and edit the values if you want a non-default web app name or region; the `.json` copy is gitignored.

### Deploy the app

The GitHub Actions workflow handles app deploys on push to `main` once the OIDC wiring is in place (follow-up PR). For a manual deploy:

```sh
cd AspNetCoreSample
dotnet publish -c Release -o publish
az webapp deploy --resource-group rg-aspnet-core-test --name <webAppName> --src-path publish --type zip
```
