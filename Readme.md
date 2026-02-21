# ⚡ EdriSys - Sistema de Gestión de Consumo Eléctrico con IoT/OCR

[![.NET](https://img.shields.io/badge/.NET-9.0-512BD4?logo=dotnet)](https://dotnet.microsoft.com/)
[![PostgreSQL](https://img.shields.io/badge/PostgreSQL-16+-4169E1?logo=postgresql&logoColor=white)](https://www.postgresql.org/)
[![Redis](https://img.shields.io/badge/Redis-7+-DC382D?logo=redis&logoColor=white)](https://redis.io/)
[![License](https://img.shields.io/badge/License-MIT-green.svg)](LICENSE)

Sistema de gestión de consumo eléctrico que automatiza la lectura de medidores mediante dispositivos IoT con reconocimiento óptico de caracteres (OCR). Desarrollado como proyecto de tesis utilizando Clean Architecture, CQRS y .NET 9.

## 📋 Tabla de Contenidos

- [Objetivo del Proyecto](#-objetivo-del-proyecto)
- [Arquitectura](#-arquitectura)
- [Módulos del Sistema](#-módulos-del-sistema)
- [Stack Tecnológico](#-stack-tecnológico)
- [Estructura del Proyecto](#-estructura-del-proyecto)
- [Instalación y Ejecución](#-instalación-y-ejecución)
- [API Endpoints](#-api-endpoints)
- [Flujo de Lectura OCR](#-flujo-de-lectura-ocr)
- [Testing](#-testing)
- [Guía de Desarrollo](#-guía-de-desarrollo)
- [Autor](#-autor)

---

## 🎯 Objetivo del Proyecto

Automatizar el proceso de lectura de medidores eléctricos mediante:

| Componente | Descripción |
|------------|-------------|
| **Dispositivos IoT** | ESP32 con cámara que capturan imágenes de medidores |
| **OCR Engine** | Extracción automática del valor de consumo (kWh) |
| **Gestión Automatizada** | Cálculo de consumo, facturación y alertas |
| **Portal Web** | Interfaz para clientes y administradores |

---

## 🏗️ Arquitectura

El proyecto implementa **Clean Architecture** (Onion Architecture) con el patrón **CQRS** (Command Query Responsibility Segregation) usando MediatR.

```
┌─────────────────────────────────────────────────────────────┐
│                        API Layer                            │
│              (Controllers, Middlewares, Filters)            │
├─────────────────────────────────────────────────────────────┤
│                    Application Layer                        │
│           (Services, Queries, ViewModels, DTOs)             │
├─────────────────────────────────────────────────────────────┤
│                      Domain Layer                           │
│    (Entities, Commands, Events, Validations, Interfaces)    │
├─────────────────────────────────────────────────────────────┤
│                   Infrastructure Layer                      │
│      (Repositories, DbContext, Configurations, Cache)       │
└─────────────────────────────────────────────────────────────┘
```

### Principios Aplicados

- **SOLID** - Single Responsibility, Open/Closed, Liskov Substitution, Interface Segregation, Dependency Inversion
- **DDD** - Domain-Driven Design (Entities, Value Objects, Domain Events)
- **CQRS** - Separación de Commands (escritura) y Queries (lectura)
- **Repository Pattern** - Abstracción del acceso a datos
- **Unit of Work** - Transacciones atómicas

---

## 📦 Módulos del Sistema

### 🔬 Core (Tesis - IoT/OCR)

| Módulo | Entidades | Descripción |
|--------|-----------|-------------|
| **IoT Devices** | `Device`, `Capture` | Gestión de dispositivos ESP32 y capturas de imágenes |
| **Readings** | `Reading`, `PeriodConsumption` | Lecturas OCR/Manual y cálculo de consumo por período |

### 🏢 Enterprise Management

| Módulo | Entidades | Descripción |
|--------|-----------|-------------|
| **Geography** | `Department`, `Province`, `District` | Ubigeo peruano normalizado (1874 distritos) |
| **Company** | `Company`, `Branch`, `Tariff` | Empresas eléctricas, sucursales y tarifas |

### 👥 Customer Management

| Módulo | Entidades | Descripción |
|--------|-----------|-------------|
| **Customer** | `Customer`, `Supply`, `Meter` | Clientes, suministros (puntos de conexión) y medidores |

### 💰 Billing

| Módulo | Entidades | Descripción |
|--------|-----------|-------------|
| **Billing** | `Invoice`, `Payment` | Facturación mensual y registro de pagos |

### 🔐 Users & Alerts

| Módulo | Entidades | Descripción |
|--------|-----------|-------------|
| **Users** | `User`, `Notification` | Autenticación JWT y notificaciones |
| **Alerts** | `Alert` | Alertas operacionales (dispositivo offline, fraude, etc.) |

---

## 🛠️ Stack Tecnológico

| Tecnología | Versión | Propósito |
|------------|---------|-----------|
| **.NET** | 9.0 | Framework principal |
| **Entity Framework Core** | 9.x | ORM y migraciones |
| **PostgreSQL** | 16+ | Base de datos relacional |
| **Redis** | 7+ | Caché distribuido |
| **MediatR** | 12.x | Patrón Mediator (CQRS) |
| **FluentValidation** | 11.x | Validación de Commands |
| **JWT Bearer** | - | Autenticación stateless |
| **Swagger/OpenAPI** | - | Documentación interactiva |
| **xUnit** | - | Framework de testing |
| **NSubstitute** | - | Mocking para tests |

---

## 📁 Estructura del Proyecto

```
EdriSys/
├── src/
│   ├── Edri.Api/                    # Capa de Presentación
│   │   ├── Controllers/             # REST Controllers
│   │   ├── Middlewares/             # Exception handling, logging
│   │   ├── Models/                  # Response models
│   │   ├── Swagger/                 # Configuración OpenAPI
│   │   └── Program.cs               # Entry point
│   │
│   ├── Edri.Application/            # Capa de Aplicación
│   │   ├── Interfaces/              # Service interfaces
│   │   ├── Services/                # Service implementations
│   │   ├── Queries/                 # Query handlers (CQRS)
│   │   ├── ViewModels/              # DTOs de entrada/salida
│   │   ├── SortProviders/           # Providers de ordenamiento
│   │   └── Extensions/              # DI extensions
│   │
│   ├── Edri.Domain/                 # Capa de Dominio
│   │   ├── Entities/                # Entidades de dominio
│   │   ├── Enums/                   # Enumeraciones
│   │   ├── Commands/                # Command handlers (CQRS)
│   │   ├── EventHandler/            # Domain event handlers
│   │   ├── Interfaces/              # Repository interfaces
│   │   ├── Notifications/           # Domain notifications
│   │   └── Extensions/              # DI extensions
│   │
│   ├── Edri.Infrastructure/         # Capa de Infraestructura
│   │   ├── Database/                # DbContext
│   │   ├── Repositories/            # Repository implementations
│   │   ├── Configurations/          # EF Core configurations + Seed
│   │   ├── Migrations/              # EF Core migrations
│   │   └── Extensions/              # DI extensions
│   │
│   └── Edri.Shared/                 # Compartido
│       └── Events/                  # Domain events
│
├── tests/
│   ├── Edri.UnitTests/              # Pruebas unitarias
│   └── Edri.IntegrationTests/       # Pruebas de integración
│
├── docker-compose.yml               # Orquestación de contenedores
├── Dockerfile                       # Imagen de la API
└── README.md
```

### Estructura de un Módulo (Ejemplo: Meter)

```
Commands (Domain)                    Queries (Application)
├── CreateMeter/                     ├── GetAll/
│   ├── CreateMeterCommand.cs        │   ├── GetAllMetersQuery.cs
│   ├── CreateMeterCommandHandler.cs │   └── GetAllMetersQueryHandler.cs
│   └── CreateMeterCommandValidation.cs
├── UpdateMeter/                     └── GetMeterById/
│   ├── UpdateMeterCommand.cs            ├── GetMeterByIdQuery.cs
│   ├── UpdateMeterCommandHandler.cs     └── GetMeterByIdQueryHandler.cs
│   └── UpdateMeterCommandValidation.cs
└── DeleteMeter/
    ├── DeleteMeterCommand.cs
    ├── DeleteMeterCommandHandler.cs
    └── DeleteMeterCommandValidation.cs
```

---

## 🚀 Instalación y Ejecución

### Prerrequisitos

- [.NET 9 SDK](https://dotnet.microsoft.com/download)
- [PostgreSQL 16+](https://www.postgresql.org/download/)
- [Redis 7+](https://redis.io/download/) (opcional, para caché)
- [Docker](https://www.docker.com/) (opcional)

### Opción 1: Ejecución Local

1. **Clonar el repositorio**
```bash
git clone https://github.com/tu-usuario/EdriSys.git
cd EdriSys
```

2. **Configurar base de datos** (`appsettings.json`)
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=EdriDb;Username=postgres;Password=Password123"
  },
  "Redis": {
    "ConnectionString": "localhost:6379"
  },
  "JwtSettings": {
    "SecretKey": "YourSuperSecretKeyWith32Characters!",
    "Issuer": "EdriSys",
    "Audience": "EdriSys.Users",
    "ExpirationMinutes": 60
  }
}
```

3. **Aplicar migraciones**
```bash
dotnet ef database update \
  --project src/Edri.Infrastructure \
  --startup-project src/Edri.Api \
  --context ApplicationDbContext
```

4. **Ejecutar la API**
```bash
dotnet run --project src/Edri.Api
```

5. **Acceder a Swagger**
```
https://localhost:5001/swagger
```

### Opción 2: Usando Docker

1. **Levantar infraestructura**
```bash
# PostgreSQL
docker run --name edri-postgres -d \
  -p 5432:5432 \
  -e POSTGRES_USER=postgres \
  -e POSTGRES_PASSWORD=Password123 \
  -e POSTGRES_DB=EdriDb \
  postgres:16

# Redis
docker run --name edri-redis -d \
  -p 6379:6379 \
  redis:7
```

2. **Construir y ejecutar API**
```bash
docker build -t edrisys-api .
docker run --name edrisys -d \
  -p 8080:8080 \
  --link edri-postgres:postgres \
  --link edri-redis:redis \
  edrisys-api
```

### Opción 3: Docker Compose (Recomendado)

```bash
# Levantar todo el stack
docker-compose up -d

# Ver logs
docker-compose logs -f api

# Detener
docker-compose down
```

**docker-compose.yml**
```yaml
version: '3.8'
services:
  postgres:
    image: postgres:16
    environment:
      POSTGRES_USER: postgres
      POSTGRES_PASSWORD: Password123
      POSTGRES_DB: EdriDb
    ports:
      - "5432:5432"
    volumes:
      - postgres_data:/var/lib/postgresql/data

  redis:
    image: redis:7
    ports:
      - "6379:6379"

  api:
    build: .
    ports:
      - "8080:8080"
    depends_on:
      - postgres
      - redis
    environment:
      - ConnectionStrings__DefaultConnection=Host=postgres;Database=EdriDb;Username=postgres;Password=Password123
      - Redis__ConnectionString=redis:6379

volumes:
  postgres_data:
```

---

## 📡 API Endpoints

### 🔐 Autenticación
| Método | Endpoint | Descripción |
|--------|----------|-------------|
| `POST` | `/api/v1/auth/login` | Iniciar sesión (devuelve JWT) |
| `POST` | `/api/v1/auth/register` | Registrar nuevo usuario |
| `POST` | `/api/v1/auth/refresh` | Renovar token |

### 📍 Geografía
| Método | Endpoint | Descripción |
|--------|----------|-------------|
| `GET` | `/api/v1/department` | Listar departamentos |
| `GET` | `/api/v1/province?departmentId={id}` | Listar provincias |
| `GET` | `/api/v1/district?provinceId={id}` | Listar distritos |

### 🏢 Empresa
| Método | Endpoint | Descripción |
|--------|----------|-------------|
| `GET/POST/PUT/DELETE` | `/api/v1/company` | CRUD Empresas |
| `GET/POST/PUT/DELETE` | `/api/v1/branch` | CRUD Sucursales |
| `GET/POST/PUT/DELETE` | `/api/v1/tariff` | CRUD Tarifas |

### 👥 Clientes
| Método | Endpoint | Descripción |
|--------|----------|-------------|
| `GET/POST/PUT/DELETE` | `/api/v1/customer` | CRUD Clientes |
| `GET/POST/PUT/DELETE` | `/api/v1/supply` | CRUD Suministros |
| `GET/POST/PUT/DELETE` | `/api/v1/meter` | CRUD Medidores |

### 📱 IoT (Core)
| Método | Endpoint | Descripción |
|--------|----------|-------------|
| `GET/POST/PUT/DELETE` | `/api/v1/device` | CRUD Dispositivos IoT |
| `GET/POST` | `/api/v1/capture` | Gestión de capturas |
| `POST` | `/api/v1/capture/{id}/process` | Procesar imagen con OCR |

### 📊 Lecturas (Core)
| Método | Endpoint | Descripción |
|--------|----------|-------------|
| `GET/POST` | `/api/v1/reading` | Gestión de lecturas |
| `GET` | `/api/v1/periodconsumption` | Consumo por período |
| `POST` | `/api/v1/periodconsumption/calculate` | Calcular consumo |

### 💰 Facturación
| Método | Endpoint | Descripción |
|--------|----------|-------------|
| `GET/POST` | `/api/v1/invoice` | Gestión de facturas |
| `GET/POST` | `/api/v1/payment` | Registro de pagos |

### 🔔 Alertas
| Método | Endpoint | Descripción |
|--------|----------|-------------|
| `GET` | `/api/v1/alert` | Listar alertas |
| `PUT` | `/api/v1/alert/{id}/resolve` | Resolver alerta |

---

## 🔄 Flujo de Lectura OCR

```
┌──────────────┐     ┌──────────────┐     ┌──────────────┐     ┌──────────────┐
│    Device    │────▶│   Capture    │────▶│   Reading    │────▶│ Consumption  │
│  (ESP32+Cam) │     │  (Imagen)    │     │  (kWh+OCR)   │     │  (Período)   │
└──────────────┘     └──────────────┘     └──────────────┘     └──────────────┘
       │                    │                    │                    │
       │                    │                    │                    │
       ▼                    ▼                    ▼                    ▼
  Instalado en         Captura cada         Extrae valor        Calcula consumo
  el medidor           hora/día             con OCR 95%+        mensual
                                            confianza
                                                                      │
                                                                      ▼
                                                              ┌──────────────┐
                                                              │   Invoice    │
                                                              │  (Factura)   │
                                                              └──────────────┘
                                                                      │
                                                                      ▼
                                                              ┌──────────────┐
                                                              │   Payment    │
                                                              │   (Pago)     │
                                                              └──────────────┘
```

### Estados del Flujo

| Entidad | Estados |
|---------|---------|
| **Device** | Active → Inactive → Maintenance |
| **Capture** | Pending → Processed / Failed |
| **Reading** | Source: OCR / Manual / Estimated |
| **Consumption** | Calculated → Billed → Adjusted |
| **Invoice** | Issued → Paid / Overdue → Voided |

---

## 🧪 Testing

### Ejecutar todas las pruebas
```bash
dotnet test
```

### Pruebas unitarias
```bash
dotnet test tests/Edri.UnitTests
```

### Pruebas de integración
```bash
dotnet test tests/Edri.IntegrationTests
```

### Con cobertura de código
```bash
dotnet test --collect:"XPlat Code Coverage"
```

### Generar reporte HTML
```bash
dotnet tool install -g dotnet-reportgenerator-globaltool
reportgenerator -reports:"**/coverage.cobertura.xml" -targetdir:"coverage-report" -reporttypes:Html
```

---

## 📖 Guía de Desarrollo

### Agregar Nueva Entidad

1. **Domain Layer**
   - [ ] Crear `Entity` en `Domain/Entities/`
   - [ ] Crear `Enums` si aplica en `Domain/Enums/`
   - [ ] Crear `IRepository` en `Domain/Interfaces/Repositories/`
   - [ ] Crear Commands en `Domain/Commands/{Entity}/`
   - [ ] Crear `EventHandler` en `Domain/EventHandler/`

2. **Shared Layer**
   - [ ] Crear Events en `Shared/Events/{Entity}/`

3. **Infrastructure Layer**
   - [ ] Crear `Repository` en `Infrastructure/Repositories/`
   - [ ] Crear `Configuration` en `Infrastructure/Configurations/`
   - [ ] Agregar `DbSet` en `ApplicationDbContext`

4. **Application Layer**
   - [ ] Crear ViewModels en `Application/ViewModels/{Entity}/`
   - [ ] Crear Queries en `Application/Queries/{Entity}/`
   - [ ] Crear `Service` en `Application/Services/`
   - [ ] Crear `SortProvider` en `Application/SortProviders/`
   - [ ] Crear `IService` en `Application/Interfaces/`

5. **API Layer**
   - [ ] Crear `Controller` en `Api/Controllers/`

6. **Dependency Injection**
   - [ ] Registrar en `Domain/Extensions/ServiceCollectionExtension.cs`
   - [ ] Registrar en `Application/Extensions/ServiceCollectionExtensions.cs`
   - [ ] Registrar en `Infrastructure/Extensions/ServiceCollectionExtensions.cs`

7. **Database**
   - [ ] Crear migración: `dotnet ef migrations add Add{Entity}`
   - [ ] Aplicar: `dotnet ef database update`

### Convenciones de Código

| Elemento | Convención | Ejemplo |
|----------|------------|---------|
| Entidad | PascalCase, singular | `Customer`, `Meter` |
| Interfaz | Prefijo I | `ICustomerRepository` |
| Command | Verbo + Entidad | `CreateCustomerCommand` |
| Query | Get + Descripción | `GetAllCustomersQuery` |
| ViewModel | Entidad + ViewModel | `CustomerViewModel` |
| Controller | Entidad + Controller | `CustomerController` |

---

## 🔐 Roles de Usuario

| Rol | Código | Permisos |
|-----|--------|----------|
| **Admin** | 1 | Acceso total al sistema |
| **Customer** | 2 | Ver su información, suministros, facturas y pagos |

---

## 📊 Modelo de Datos

```
┌─────────────────────────────────────────────────────────────────────────────┐
│                              GEOGRAPHY                                       │
│  Department (25) ─── Province (196) ─── District (1874)                     │
└─────────────────────────────────────────────────────────────────────────────┘
                                    │
                    ┌───────────────┴───────────────┐
                    ▼                               ▼
┌─────────────────────────────┐     ┌─────────────────────────────┐
│     ENTERPRISE              │     │     CUSTOMER                │
│  Company ─── Branch         │     │  Customer ─── Supply ─── Meter
│      └────── Tariff         │     │                    │        │
└─────────────────────────────┘     └────────────────────┼────────┘
                                                         │
                    ┌────────────────────────────────────┤
                    ▼                                    ▼
┌─────────────────────────────┐     ┌─────────────────────────────┐
│     IoT DEVICES (CORE)      │     │     READINGS (CORE)         │
│  Device ─── Capture         │────▶│  Reading ─── PeriodConsumption
└─────────────────────────────┘     └─────────────────────────────┘
                                                         │
                                                         ▼
                                    ┌─────────────────────────────┐
                                    │     BILLING                 │
                                    │  Invoice ─── Payment        │
                                    └─────────────────────────────┘
```

**Total: 15 Entidades**

---

## 🤝 Contribuir

1. Fork el proyecto
2. Crear rama feature (`git checkout -b feature/AmazingFeature`)
3. Commit cambios (`git commit -m 'Add: AmazingFeature'`)
4. Push a la rama (`git push origin feature/AmazingFeature`)
5. Abrir Pull Request

---

## 👨‍💻 Autor

**[GK-PE]**
- 🎓 Tesis: *"Sistema de Gestión de Consumo Eléctrico mediante Dispositivos IoT con Reconocimiento Óptico de Caracteres"*
- 🏛️ Universidad: UNAS
- 📅 Año: 2026

---

## 📄 Licencia

Este proyecto está bajo la Licencia MIT - ver el archivo [LICENSE](LICENSE) para más detalles.

---

<div align="center">

⚡ **EdriSys** - Automatizando la lectura de consumo eléctrico con IoT y OCR

[![Made with .NET](https://img.shields.io/badge/Made%20with-.NET%209-512BD4?logo=dotnet)](https://dotnet.microsoft.com/)

</div>