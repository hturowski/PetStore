pipeline {
    agent any
	environment{
		DOCKERNAME="petstore_${env.BRANCH_NAME}:${env.BUILD_NUMBER}"
		DBNAME="petstore_${env.BRANCH_NAME}"
	}
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
				bat "docker build -t ${env.DOCKERNAME} ."
            }
        }
        stage('Deploy') {
            steps {
				bat "kubectl --kubeconfig c:\\Users\\hturowski\\.kube\\config set image deployments/rest-test rest-test=${env.DOCKERNAME}"
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
