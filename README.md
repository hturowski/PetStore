# PetStore

## Overview
A simple REST service meant to be deployed using Kuberntes. This services requires a MySQL instance which can be deployed using the included PetStore-MySQL.yml file

## Set up locally on Windows 10

### Docker and Kubernetes
[Docker for Windows](https://docs.docker.com/docker-for-windows/) must be installed on your workstation before you begin. Instead of downloading from the Windows Store, use the link to download.docker.com in the [Install Docker for Windows desktop app section of this page](https://docs.docker.com/docker-for-windows/).
After installation, make sure Kubernetes is enabled in the Settings section of Docker for Windows.

You can test your installation by running `kubectl run nginx --image=nginx`. This will pull an nginx Docker image from the official Docker repository and store it in your local Docker repository. Kubernetes will then deploy it to your local Kubernetes cluster.
Use `docker images` to verify that a container named `nginx` appears in your local repository, then `kubectl get deployments` and `kubectl get pods` to verify that the nginx service is deployed to Kubernetes.

Remove the nginx deployment using `kubectl delete deployment nginx`.

### MySQL
Do not install MySQL on your local workstation. If you have MySQL installed, stop the service and set it to start manually. When you deploy the MySQL instance to Kubernetes it will use default MySQL port port `3306` so having a local instance running will cause problems.

You will need a way to interact with MySQL. You can use [MySQL Workbench](https://www.mysql.com/products/workbench/) or your favorite SQL tools.

### .NET Core
You will need the [.NET SDK](https://dotnet.github.io/) and associated tools. Install them now.


