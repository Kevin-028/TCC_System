#include <SoftwareSerial.h>

SoftwareSerial espSerial(2, 3);  // RX, TX para comunicação com o ESP-01

String ssid = "Kevin";          // Nome da rede Wi-Fi
String password = "Senha123";   // Senha da rede Wi-Fi
String apiUrl = "gerencia360.com.br"; // URL da API para teste

void setup() {
  Serial.begin(9600);     // Inicia a comunicação serial com o monitor serial
  espSerial.begin(9600);  // Inicia a comunicação serial com o ESP-01
  
  delay(2000);

  // Conecta ao Wi-Fi
  connectToWiFi();

  // Envia a requisição HTTP POST
  sendHttpRequest();

  // Espera mais tempo para capturar a resposta do servidor
  String response = "";
  unsigned long startTime = millis();
  while (millis() - startTime < 20000) {  // Espera até 20 segundos
    if (espSerial.available()) {
      response += espSerial.readString();
    }
  }
  
  if (response.length() > 0) {
    Serial.println("Resposta do servidor:");
    Serial.println(response);  // Exibe a resposta no Serial Monitor
  } else {
    Serial.println("Sem resposta do servidor.");
  }
}

void loop() {
  // Se o ESP-01 enviar dados, imprima no Serial Monitor
  if (espSerial.available()) {
    String response = espSerial.readString();
    Serial.println(response);  // Exibe a resposta no Serial Monitor
  }
}

// Função para conectar ao Wi-Fi
void connectToWiFi() {
  String cmd = "AT+CWJAP=\"" + ssid + "\",\"" + password + "\"";
  sendCommand(cmd, 10000);  // Espera até 10 segundos para conectar
  Serial.println("Conectado ao Wi-Fi");
}

// Função para enviar requisição HTTP POST
void sendHttpRequest() {
  // Conecta ao servidor
  sendCommand("AT+CIPSTART=\"TCP\",\"gerencia360.com.br\",80", 5000);

  // Aguarda para garantir que a conexão foi estabelecida
  delay(5000);

  // Monta a requisição POST
  // Monta a requisição GET
  String postRequest = "GET /api/TccSystem/TryMessage/41282062-67ea-4d26-983a-1013cae7a7a2 HTTP/1.1\r\n";
  postRequest += "Host: " + apiUrl + "\r\n";
  postRequest += "Connection: close\r\n\r\n";
  
  // Envia o comando para iniciar o envio da requisição
  String sendCmd = "AT+CIPSEND=" + String(postRequest.length() + 2);  // O +2 é por conta do "\r\n"
  sendCommand(sendCmd, 1000);  // Envia o comando
  
  delay(500);  // Aguarda um pouco antes de enviar o corpo
  
  // Envia a requisição POST
  sendCommand(postRequest, 5000);
  
  // Aguarda e captura a resposta
  String response = "";
  unsigned long startTime = millis();
  while (millis() - startTime < 20000) {  // Espera até 20 segundos
    if (espSerial.available()) {
      response += espSerial.readString();
    }
  }
  
  if (response.length() > 0) {
    Serial.println("Resposta do servidor:");
    Serial.println(response);  // Exibe a resposta no Serial Monitor
  } else {
    Serial.println("Sem resposta do servidor.");
  }
}

// Função para enviar comandos AT e ler a resposta
void sendCommand(String command, int timeout) {
  espSerial.println(command);
  long int time = millis();
  while (millis() - time < timeout) {
    while (espSerial.available()) {
      String response = espSerial.readString();
      Serial.println(response);  // Exibe a resposta do ESP-01 no Serial Monitor
    }
  }
}
