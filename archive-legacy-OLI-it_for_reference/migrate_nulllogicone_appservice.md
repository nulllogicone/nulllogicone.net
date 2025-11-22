# Migrate legacy nulllogicone web app with custom domain name

Old Environment Variables
```json
[
  {
    "name": "APPINSIGHTS_INSTRUMENTATIONKEY",
    "value": "923c7b27-a1b1-48c2-8d72-6034302b9efe",
    "slotSetting": true
  },
  {
    "name": "APPINSIGHTS_PROFILERFEATURE_VERSION",
    "value": "1.0.0",
    "slotSetting": true
  },
  {
    "name": "APPINSIGHTS_SNAPSHOTFEATURE_VERSION",
    "value": "1.0.0",
    "slotSetting": true
  },
  {
    "name": "APPLICATIONINSIGHTS_CONNECTION_STRING",
    "value": "InstrumentationKey=923c7b27-a1b1-48c2-8d72-6034302b9efe;IngestionEndpoint=https://eastus-6.in.applicationinsights.azure.com/;LiveEndpoint=https://eastus.livediagnostics.monitor.azure.com/;ApplicationId=237eb9df-f3b7-4d20-8c61-218c7a111fdb",
    "slotSetting": false
  },
  {
    "name": "ApplicationInsightsAgent_EXTENSION_VERSION",
    "value": "~2",
    "slotSetting": true
  },
  {
    "name": "AzureWebJobsDashboard",
    "value": "DefaultEndpointsProtocol=https;AccountName=oliit;AccountKey=PoRUZi2ox5Hne86EW2e6CP/docIxtOJ0DqOqH+1/pRByCOvI6iJzXTsHY9bI/Rxo+KvUZFuu+FsQ6JqRjNt/lA==",
    "slotSetting": false
  },
  {
    "name": "bilderOrdner",
    "value": "https://oliit.blob.core.windows.net/oliupload",
    "slotSetting": false
  },
  {
    "name": "DiagnosticServices_EXTENSION_VERSION",
    "value": "~3",
    "slotSetting": true
  },
  {
    "name": "InstrumentationEngine_EXTENSION_VERSION",
    "value": "disabled",
    "slotSetting": true
  },
  {
    "name": "letsencrypt:ClientId",
    "value": "680622f1-34e4-4e21-a1bd-6dbd89d76525",
    "slotSetting": false
  },
  {
    "name": "letsencrypt:ClientSecret",
    "value": "yUaqjD+CVyqWRZ+PVHMjSnVvVTWPQmjAIps988fNoN8=",
    "slotSetting": false
  },
  {
    "name": "letsencrypt:ResourceGroupName",
    "value": "Default-Web-WestEurope",
    "slotSetting": false
  },
  {
    "name": "letsencrypt:ServicePlanResourceGroupName",
    "value": "Default-Web-WestEurope",
    "slotSetting": false
  },
  {
    "name": "letsencrypt:SiteSlot",
    "value": "",
    "slotSetting": false
  },
  {
    "name": "letsencrypt:SubscriptionId",
    "value": "33dd8226-abb3-4f36-b1f0-059e18b9570a",
    "slotSetting": false
  },
  {
    "name": "letsencrypt:Tenant",
    "value": "zoschkeluchting.onmicrosoft.com",
    "slotSetting": false
  },
  {
    "name": "letsencrypt:UseIPBasedSSL",
    "value": "false",
    "slotSetting": false
  },
  {
    "name": "MSDEPLOY_RENAME_LOCKED_FILES",
    "value": "1",
    "slotSetting": false
  },
  {
    "name": "SnapshotDebugger_EXTENSION_VERSION",
    "value": "disabled",
    "slotSetting": true
  },
  {
    "name": "WEBSITE_NODE_DEFAULT_VERSION",
    "value": "0.10.32",
    "slotSetting": false
  },
  {
    "name": "XDT_MicrosoftApplicationInsights_BaseExtensions",
    "value": "disabled",
    "slotSetting": true
  },
  {
    "name": "XDT_MicrosoftApplicationInsights_Java",
    "value": "disabled",
    "slotSetting": false
  },
  {
    "name": "XDT_MicrosoftApplicationInsights_Mode",
    "value": "recommended",
    "slotSetting": true
  },
  {
    "name": "XDT_MicrosoftApplicationInsights_NodeJS",
    "value": "disabled",
    "slotSetting": true
  },
  {
    "name": "XDT_MicrosoftApplicationInsights_PreemptSdk",
    "value": "disabled",
    "slotSetting": true
  }
]
```

