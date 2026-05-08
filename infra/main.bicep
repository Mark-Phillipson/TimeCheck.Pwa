@description('A short prefix for resource names (<=3 alphanumeric chars)')
param resourcePrefix string = 'tc'

@description('Deployment environment name')
param envName string = 'prod'

@description('Location for resources')
param location string = resourceGroup().location

@description('SKU for Static Web App')
@allowed([ 'Free', 'Standard' ])
param skuName string = 'Free'

var resourceToken = uniqueString(subscription().id, resourceGroup().id, location, envName)
var identityName = 'az' + resourcePrefix + resourceToken + 'id'
var staticSiteName = 'az' + resourcePrefix + resourceToken + 'swa'

resource userIdentity 'Microsoft.ManagedIdentity/userAssignedIdentities@2018-11-30' = {
  name: identityName
  location: location
}

resource staticSite 'Microsoft.Web/staticSites@2024-11-01' = {
  name: staticSiteName
  location: location
  identity: {
    type: 'UserAssigned'
    userAssignedIdentities: {
      '${userIdentity.id}': {}
    }
  }
  sku: {
    name: skuName
    tier: skuName
  }
  properties: {
    buildProperties: {
      appLocation: '/'
      apiLocation: ''
      outputLocation: 'publish/wwwroot'
    }
    publicNetworkAccess: 'Enabled'
  }
  dependsOn: [
    userIdentity
  ]
}

output staticSiteEndpoint string = 'https://${staticSite.properties.defaultHostname}'
output staticSiteResourceId string = staticSite.id
output userAssignedIdentityId string = userIdentity.id
>>>>>>> main
@description('A short prefix for resource names (<=3 alphanumeric chars)')
param resourcePrefix string = 'tc'

@description('Deployment environment name')
param envName string = 'prod'

@description('Location for resources')
param location string = resourceGroup().location

@description('SKU for Static Web App')
@allowed([ 'Free', 'Standard' ])
param skuName string = 'Free'

var resourceToken = uniqueString(subscription().id, resourceGroup().id, location, envName)
var identityName = 'az' + resourcePrefix + resourceToken + 'id'
var staticSiteName = 'az' + resourcePrefix + resourceToken + 'swa'

resource userIdentity 'Microsoft.ManagedIdentity/userAssignedIdentities@2018-11-30' = {
  name: identityName
  location: location
}

resource staticSite 'Microsoft.Web/staticSites@2024-11-01' = {
  name: staticSiteName
  location: location
  identity: {
    type: 'UserAssigned'
    userAssignedIdentities: {
      '${userIdentity.id}': {}
    }
  }
  sku: {
    name: skuName
    tier: skuName
  }
  properties: {
    buildProperties: {
      appLocation: '/'
      apiLocation: ''
      outputLocation: 'publish/wwwroot'
    }
    publicNetworkAccess: 'Enabled'
  }
  dependsOn: [
    userIdentity
  ]
}

output staticSiteEndpoint string = 'https://${staticSite.properties.defaultHostname}'
output staticSiteResourceId string = staticSite.id
output userAssignedIdentityId string = userIdentity.id
>>>>>>> main
