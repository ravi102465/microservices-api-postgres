$param1=$args[0]

if ($param1 -eq 'delete')
{
    kubectl config set-context --current --namespace=application-ns
    #kubectl delete -f .\05-ingress.yaml
    kubectl delete -f .\04-filmapi-deployment-azure.yaml
    kubectl delete -f .\03-filmapi-service-azure.yaml
    kubectl delete -f .\02-configMap.yaml
    kubectl delete -f .\01-namespace.yaml
}
elseif ($param1 -eq 'apply')
{
    kubectl apply -f .\01-namespace.yaml
    kubectl config set-context --current --namespace=application-ns
    kubectl apply -f .\02-configMap.yaml
    kubectl apply -f .\03-filmapi-service-azure.yaml
    kubectl apply -f .\04-filmapi-deployment-azure.yaml
    #kubectl apply -f .\05-ingress.yaml
}
else
{
	Write-Host 'Invalid param'
}
