pipeline {
    agent any

    stages {
        stage('Running unit tests') {
            steps {
                echo 'Unit testing..'
                bat "dotnet test"
            }
        }
        stage('Database Migration') {
            steps {
                echo 'Applying database migrations..'
                bat "cd PetStore;dotnet ef --startup-project .\\PetStore\\ database update;cd.."
            }
        }
        stage('Build Docker Container') {
            steps {
                echo 'Building..'
		bat "docker build -t petstore:${env.BUILD_NUMBER} ."
            }
        }
        stage('Test') {
            steps {
                echo 'Testing..'
            }
        }
        stage('Deploy') {
            steps {
		bat "kubectl --kubeconfig c:\\Users\\hturowski\\.kube\\config set image deployments/rest-test rest-test=petstore:${env.BUILD_NUMBER}"
            }
        }
    }
}
