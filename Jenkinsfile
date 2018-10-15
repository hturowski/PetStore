pipeline {
    agent any

    stages {
        stage('Build Docker Container') {
            steps {
                echo 'Building..'
				bat "docker build -t rest-test:${env.BUILD_NUMBER} ."
            }
        }
        stage('Test') {
            steps {
                echo 'Testing..'
            }
        }
        stage('Deploy') {
            steps {
				bat "kubectl --kubeconfig c:\\Users\\hturowski\\.kube\\config set image deployments/rest-test rest-test=rest-test:${env.BUILD_NUMBER}"
            }
        }
    }
}
