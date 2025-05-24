# UnivMVC

Aplicación Web MVC construido con ASP.NET Core. Desarrollado bajo principios de arquitectura limpia, con soporte para contenedores Docker. Consume UnivAuth, UnivAcademico y UnivCtaCte.

## 🚀 Características

- Aplicación Web MVC con Razor Pages.
- Reqiere autenticación de doble factor.
- Consume microservicios de autenticación, información académica y cuentas corrientes.
- Arquitectura por capas (Domain, Application, Infrastructure, API).
- Preparado para despliegue en Docker.

---

## 🛠️ Tecnologías utilizadas

- .NET 8
- OtpNet para TFA
- Docker & Docker Compose
- Clean Architecture (Separación de responsabilidades)

---

## 📦 Docker Compose

```yaml
services:
  api:
    build:
      context: .
      dockerfile: UnivMVC.Web/Dockerfile
    ports:
      - "5000:8080"
```

---

## ▶️ Cómo ejecutar

1. Clonar este repositorio:
   ```bash
   git clone https://github.com/Charlie-Nash/UnivMVC
   cd UnivMVC
   ```

2. Ejecutar con Docker Compose:
   ```bash
   sudo docker-compose up --build -d
   o
   sudo docker compose up --build -d
   ```
---

## 👤 Autor

Desarrollado por **Charlie Nash**

---

## 📄 Licencia

Este proyecto se puede usar libremente para fines educativos o personales.