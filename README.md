# PetStore

## Overview
A simple REST service meant to be deployed using Kubernetes.
This services requires a MySQL instance which can be deployed using the included `PetStore-MySQL.yml` file.

### What This Proof of Concept Doesn't Do (Yet)
#### There is no automatic cleanup
When a branch is deleted, the Kubernetes namespace and related database are not removed.
#### There is no Kubernetes ingress controller
Currently the `default` namespace is production, the `master` namespace is for integration testing, and the branch namespace is for feature development.
Each runs on its own port, with `default` on port 80, `master` on port `81` and all branches on port `82`.
All services should be running on port `80` with an ingress controller routing traffic appropriately, probably based on header information included in the namespace.
This means that in the current state, this proof of concept only supports one feature branch, and manual cleanup is required before a new one can be deployed.

### A Warning About Port Conflicts
You will be installing services which will run on ports `80`, `81`, `82`, `8080`, and `3306`.
If you already have services running on any of these ports, you will need to stop them in order for things to work.

## Prerequisites for Local Setup on Windows 10

### Docker and Kubernetes
[Docker for Windows](https://docs.docker.com/docker-for-windows/) must be installed on your workstation before you begin.
Instead of downloading from the Windows Store, use the link to download.docker.com in the [Install Docker for Windows desktop app section of this page](https://docs.docker.com/docker-for-windows/).
After installation, make sure Kubernetes is enabled in the Settings section of Docker for Windows.

You can test your installation by running `kubectl run nginx --image=nginx`.
This will pull an nginx Docker image from the official Docker repository and store it in your local Docker repository.
Kubernetes will then deploy it to your local Kubernetes cluster.
Use `docker images` to verify that a container named `nginx` appears in your local repository, then `kubectl get deployments` and `kubectl get pods` to verify that the nginx service is deployed to Kubernetes.

Remove the nginx deployment using `kubectl delete deployment nginx` and `kubectl delete service nginx`.

After you have Kubernetes working, you'll need to copy (not move) the `.kube` folder in your user's home directory to the root of your `C` drive.
After the copy, Jenkins will use the Kubernetes configuration file at `c:\.kube\config` to manage your local Kubernetes cluster.

### MySQL Workbench
**Do not install MySQL on your local workstation.**
If you have MySQL installed, stop the service and set it to start manually.
When you deploy the MySQL instance to Kubernetes it will use default MySQL port `3306` so having a local instance running will cause problems.

You will need a way to interact with MySQL.
You can use [MySQL Workbench](https://www.mysql.com/products/workbench/) or your favorite SQL tools.

### .NET Core
You will need the [.NET SDK](https://dotnet.github.io/) and associated tools.
Install them now.

### Jenkins
You will need a local Jenkins instance to perform automated deployments of your service.
Install [Jenkins for Windows](https://jenkins.io/download/) and make sure you choose to install the `Multibranch Pipeline` plugin.
You can leave the other options alone, or remove things you know you won't need like `SVN` support.
Jenkins will install as a Windows service and start automatically.
Once installed, you will find Jenkins at `http://localhost:8080/` 

### Ruby
The deployment process uses a Ruby script to generate Kubernetes deployment files.
You will need to have Ruby installed and available to all users on your system.
This means that `ruby.exe` needs to be in the system's `PATH`.
After you clone the repository, open a terminal window in the `Ruby` directory and run `ruby install bundler` followed by `bundle install`.

## Deploy MySQL to your Kubernetes Cluster
In the PetStore subdirectory there is a file named `PetStore-MySQL.yml`.
This contains deployment information for the MySQL database which will be the back end for our service.
From the PetStore subdirectory run `kubectl create -f PetStore-MySQL.yml` to create the database instance.
The database will be accessible to your local machine on port `3306`, with username `root` and password `root` (I know... please don't send me emails).
Launch MySQL Workbench and verify that you can connect to the database.

## Configure Jenkins to Monitor the Repository
Open Jenkins at http://localhost:8080/ and create a `New Item`.
Choose a name for your job and select `Multibranch Pipeline` then click `Ok`.
In the `Branch Sources` section, set the Project Repository to this repository's address.
Add your GitHub credentials and select them in the `Credentials` dropdown.
Click `Save`.
