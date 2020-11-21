pipeline {
  agent any
  stages {
    stage('Build') {
      steps {
        sh '"C:\\Program Files\\Unity\\Hub\\Editor\\2019.4.12f1\\Editor\\Unity.exe" -batchmode -nographics -projectPath "$(pwd)" -logFile unitylog.log -quit'
      }
    }

  }
}