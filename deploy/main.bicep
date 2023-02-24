param location string = resourceGroup().location
@allowed([ 'ci', 'tst', 'acc', 'prd' ])
param environment string
param project_name string = 'alexandria'

param swa_sku string
param law_sku string
param law_retentionInDays int
param law_dailyQuotaGb int

var law_name = 'law-${project_name}-${environment}'
var swa_name = 'swa-${project_name}-${environment}'
var ai_name = 'ai-${project_name}-${environment}'

module law './modules/law.bicep' = {
  name: '${project_name}-law'
  params: {
    projectname: project_name
    location: location
    name: law_name
    dailyQuotaGb: law_dailyQuotaGb
    retentionInDays: law_retentionInDays
    sku: law_sku
  }
}

module ai './modules/ai.bicep' = {
  name: '${project_name}-insights'
  params: {
    projectname: project_name
    location: location
    name: ai_name
    workspaceResourceId: law.outputs.workspaceResourceId
  }
}

module swa './modules/swa.bicep' = {
  name: '${project_name}-swa'
  params: {
    projectname: project_name
    location: location
    name: swa_name
    sku: swa_sku
    application_insights_instrumentation_key: ai.outputs.instrumentationKey
    application_insights_connection_string: ai.outputs.connectionString
  }
}

output deployment_token string = swa.outputs.deployment_token
