pipeline {
    agent any
    stages {
       stage('Database Migration') {
            steps {
                echo 'Applying database migrations..'
				dir("PetStore") {
                	bat "dotnet ef database update"
				}
            }
        }
        stage('Unit Tests') {
            steps {
                echo 'Unit testing..'
                bat "dotnet test ./PetStore.Tests"
            }
        }

        stage('Build Docker Container') {
            steps {
                echo 'Building..'
				bat "docker build -t petstore:${env.BUILD_NUMBER} ."
            }
        }
        stage('Deploy') {
            steps {
				bat "kubectl --kubeconfig c:\\Users\\hturowski\\.kube\\config set image deployments/rest-test rest-test=petstore:${env.BUILD_NUMBER}"
            }
        }
        stage('Integration Test') {
            steps {
                echo 'Integration Testing..'
                bat "dotnet test ./PetStore.Integration.Tests"
            }
			post {
				failure {
					echo 'Rolling back..'
					bat "kubectl rollout undo deployments/rest-test"
				}
			}
        }
    }
}
