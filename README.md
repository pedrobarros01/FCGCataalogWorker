# 🚀 FIAP-Cloud-Games Catalog Worker

Esse worker foi construido para compor o projeto da API Catalog, com o intuito de realizar a ultima etapa do fluxo de 
compra do usuário.


---

## 📌 Segunda fase

Essa fase consiste em refatorar o monolitico feito na primeira fase em uma arquitetura de microsserviços, contendo os 
seguintes microsserviços

* Microsserviço de Usuários (UsersAPI)
* Microsserviço de Catálogo (CatalogAPI e CatalogWorker)
* Microsserviço de Pagamentos (PaymentsWorker)
* Microsserviço de Notificações (NotificationsWorker)

## Imagem no docker
A imagem desse projeto está disponível no Docker Hub como `drungas/fcgcatalogworker`. As variaveis de ambientes são 
mostradas na seção de `Como rodar o projeto via docker compose`


---

## ⚙️ Tecnologias

* .NET 10
* Worker Service
* Entity Framework Core
* PostgreSQL

---

## 📦 Pré-requisitos

Antes de começar, você precisa ter instalado:

* .NET SDK 10
* Banco de dados (PostgreSQL)

---

## ▶️ Como rodar o projeto localmente

### 1. Clonar o repositório

```bash
git clone --recurse-submodules https://github.com/pedrobarros01/FCGCatalogWorker.git
cd FCGCatalogWorker
```

### 2. Configurar variáveis de ambiente

Cri e modifique o arquivo `appsettings.json` para o template abaixo:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=portaDoBanco;Database=seubanco;Username=seuusuario;Password=suasenhga"
  },
  "RabbitMQ": {
    "Host": "localhost",
    "VirtualHost": "my_vhost",
    "Username": "usuario_rabbit",
    "Password": "senha_rabbit",
    "KeyQueuePaymentProcessed": "nome_da_fila"
  }

}
```

### 3. Executar o worker

```bash
cd src/FCG.Catalog.Worker
dotnet run
```

Com o postgres e o rabbit rodando, o worker estará rodando.

---
## ▶️ Como rodar o projeto via docker compose

### 1. Clonar o repositório

```bash
git clone --recurse-submodules https://github.com/pedrobarros01/FCGCatalogWorker.git
cd FCGCatalogWorker
```

### 2. Configurar variáveis de ambiente

Acesse o arquivo `docker-compose.yml` e modifique as seguintes variáveis de ambiente abaixo:
```
Chave da imagem Rabbit:
RABBITMQ_DEFAULT_USER: usuario_rabbit
RABBITMQ_DEFAULT_PASS: senha_rabbit
RABBITMQ_DEFAULT_VHOST: vhost_habbit

Chave do banco:
ConnectionStrings__DefaultConnection: "Host=;Port=;Database=;Username=;Password="

Chaves do Rabbit no projeto:
RabbitMQ__VirtualHost: vhost_habbit
RabbitMQ__Username: usuario_rabbit
RabbitMQ__Password: senha_rabbit
RabbitMQ__KeyQueuePaymentProcessed: "nome_da_fila"
```
> É necessário usar a mesma chave de banco do repositório: https://github.com/SergioHMagalhaes/FCG-Catalog

### 3. Executar o compose
#### 3.1 Subir os containers
```bash
cd FCGCatalogWorker
docker compose up
```

#### 3.2 Descer os containers
```bash
docker compose down
```
---
## ▶️ Como rodar o projeto via kubernetes

### 1. Clonar o repositório

```bash
git clone --recurse-submodules https://github.com/pedrobarros01/FCGCatalogWorker.git
cd FCGCatalogWorker
```

### 2. Configurar variáveis de ambiente

Acesse o arquivo `k8s/fcg-catalog-worker-configmap.yml` e modifique a variáveis de ambiente abaixo:
```
RabbitMQ__KeyQueuePaymentProcessed: "nome_da_fila"
```

Acesse o arquivo `k8s/fcg-catalog-worker-secret.yml` e modifique a variáveis de ambiente abaixo:
```
ConnectionStrings__DefaultConnection: "Host=;Port=;Database=;Username=;Password="
RabbitMQ__Host: host_rabbit
RabbitMQ__VirtualHost: vhost_habbit
RabbitMQ__Username: usuario_rabbit
RabbitMQ__Password: senha_rabbit
```
> É necessário usar a mesma variável de ambiente de banco do repositório: https://github.com/SergioHMagalhaes/FCG-Catalog

> É necessário subir um serviço de rabbitMQ para poder rodar o projeto.

### 3. Executar o projeto
#### 3.1 Aplicar o kubernetes
```bash
cd FCGCatalogWorker
kubectl apply -f fcg-catalog-worker-configmap.yml
kubectl apply -f fcg-catalog-worker-secret.yml
kubectl apply -f fcg-catalog-worker-deployment.yml
```

#### 3.2 Deletar o Configmap, Secret e Deployment
```bash
kubectl delete configmap fcg-catalog-worker-config
kubectl delete secret fcg-catalog-worker-secret
kubectl delete deployment fcg-catalog-worker-deployment
```

---

## 🗂️ Estrutura do projeto

```
/src
 ├── FCG.Catalog.Worker
 ├── FCG.Catalog.Worker.Application
 ├── FCG.Catalog.Worker.Domain
 ├── FCG.Catalog.Worker.Infrastructure
 |── FCG.Catalog.Worker.Logger
```

---

## 🧪 Testes

```bash
dotnet test
```

---

## 🤝 Contribuição

1. Crie uma branch:

```bash
git checkout -b feature/minha-feature
```

2. Commit:

```bash
git commit -m "feat: minha nova feature"
```

3. Push:

```bash
git push origin minha-feature
```

---


## 📞 Contato

O time responsável desse sistema:
- Igor Anthony - igor.anthony.iop@gmail.com
- Nathalia Greice - nponce410@gmail.com
- Otávio de Andrade - otavio_andrade@live.com
- Pedro Henrique Barros - pedrobarros0101@outlook.com
- Sérgio Henrique - ssergioh3@gmail.com