pipeline {
    agent any

    stages {
        stage('Build Docker Container') {
            steps {
                echo 'Building..'
				bat "docker build -t petservice:${env.BUILD_NUMBER} ."
            }
        }
        stage('Test') {
            steps {
                echo 'Testing..'
            }
        }
        stage('Deploy') {
            steps {
				bat "kubectl --kubeconfig c:\\Users\\hturowski\\.kube\\config set image deployments/rest-test rest-test=petservice:${env.BUILD_NUMBER}"
            }
        }
    }
}
