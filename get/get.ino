#include <SoftwareSerial.h>
#include <ArduinoJson.h>

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

  // Envia a requisição HTTP GET
  sendHttpRequest();

  // Captura e processa a resposta JSON
  String jsonResponse = captureResponse();
  if (jsonResponse.length() > 0) {
    parseJson(jsonResponse);
  }
}

void loop() {
  // A função loop está vazia, pois a resposta é tratada no setup()
}

// Função para conectar ao Wi-Fi
void connectToWiFi() {
  String cmd = "AT+CWJAP=\"" + ssid + "\",\"" + password + "\"";
  sendCommand(cmd, 10000);  // Espera até 10 segundos para conectar
  Serial.println("Conectado ao Wi-Fi");
}

// Função para enviar requisição HTTP GET
void sendHttpRequest() {
  // Conecta ao servidor
  sendCommand("AT+CIPSTART=\"TCP\",\"" + apiUrl + "\",80", 5000);

  // Aguarda para garantir que a conexão foi estabelecida
  delay(5000);

  // Monta a requisição GET
  String getRequest = "GET /api/TccSystem/ProjectModule/1e51a034-da7d-430c-85c0-bc292d638d2e HTTP/1.1\r\n";
  getRequest += "Host: " + apiUrl + "\r\n";
  getRequest += "Connection: close\r\n\r\n";
  
  // Envia o comando para iniciar o envio da requisição
  String sendCmd = "AT+CIPSEND=" + String(getRequest.length());
  sendCommand(sendCmd, 1000);  // Envia o comando para iniciar a transmissão
  
  delay(500);  // Aguarda um pouco antes de enviar o corpo
  
  // Envia a requisição GET
  sendCommand(getRequest, 5000);
}

// Função para capturar e retornar a resposta JSON do servidor
String captureResponse() {
  String response = "";
  unsigned long startTime = millis();

  // Lê enquanto houver dados disponíveis e a conexão não tiver fechado
  while (millis() - startTime < 20000) {  // Espera até 20 segundos
    if (espSerial.available()) {
      char c = espSerial.read();
      response += c;
    }
  }

  // Localiza o início do JSON na resposta
  int jsonStart = response.indexOf('{');
  if (jsonStart != -1) {
    return response.substring(jsonStart);  // Retorna apenas o conteúdo JSON
  } else {
    Serial.println("JSON não encontrado na resposta.");
    return "";
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

// Função para analisar e exibir o JSON
void parseJson(String jsonResponse) {
  // Cria um objeto JSON dinâmico com um tamanho de buffer suficiente
  DynamicJsonDocument doc(1024);

  // Analisa o JSON
  DeserializationError error = deserializeJson(doc, jsonResponse);
  if (error) {
    Serial.print("Erro ao analisar JSON: ");
    Serial.println(error.c_str());
    return;
  }

  // Extrai e exibe os dados do JSON
  Serial.println("Dados do Projeto:");
  Serial.print("ID: ");
  Serial.println(doc["Id"].as<String>());
  Serial.print("Nome: ");
  Serial.println(doc["Name"].as<String>());
  Serial.print("Nid: ");
  Serial.println(doc["Nid"].as<int>());
  Serial.print("UserID: ");
  Serial.println(doc["UserId"].as<int>());

  // Lê e exibe cada módulo no array "Modules"
  JsonArray modules = doc["Modules"].as<JsonArray>();
  for (JsonObject module : modules) {
    Serial.println("Módulo:");
    Serial.print("  ProjectId: ");
    Serial.println(module["ProjectId"].as<String>());
    Serial.print("  ModuleId: ");
    Serial.println(module["ModuleId"].as<String>());
    Serial.print("  Tipo: ");
    Serial.println(module["Type"].as<String>());
    Serial.print("  Ativo: ");
    Serial.println(module["Active"].as<bool>());
    Serial.print("  Valor: ");
    Serial.println(module["value"].as<String>());
    Serial.print("  Confiança: ");
    Serial.println(module["confidence"].as<float>());
  }
}
