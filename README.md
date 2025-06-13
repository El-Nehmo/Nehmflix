# 🎬 NehmFlix

NehmFlix est une application web complète permettant de rechercher des films et séries via l’API TMDb, de créer un compte, et de gérer sa propre **watchlist**.  
Elle est construite avec :

- **Angular 19** pour le front-end  
- **.NET 8 (C#)** pour le back-end  
- **MySQL** pour la base de données  
- **Bootstrap** pour le style  
- **TMDb API** pour les données externes  
- **BCrypt.Net** pour sécuriser les mots de passe

---

## 📋 Prérequis

### 🔧 Outils à installer (obligatoires)

| Outil         | Pourquoi ?                            | Téléchargement                             |
|---------------|----------------------------------------|--------------------------------------------|
| **Node.js**   | Pour faire tourner Angular             | https://nodejs.org/en                      |
| **Angular CLI** | Pour gérer et compiler le front-end  | `npm install -g @angular/cli`              |
| **.NET SDK 8** | Pour exécuter l’API back-end en C#    | https://dotnet.microsoft.com/download      |
| **MySQL**     | Pour stocker les utilisateurs et watchlist | https://www.mysql.com/fr/              |

---

## 🧪 Installation complète

### 🔁 Étape 1 - Cloner le projet

```bash
git clone https://github.com/El-Nehmo/Nehmflix.git
cd Nehmflix
```

---

## 🔹 Front-End (Angular)

### 📦 Installation des dépendances

```bash
npm install
```

### 🔑 Configuration de la clé TMDb

Dans `src/environments/environment.ts`, ajoute ta clé TMDb : (au préalable créer un compte tmdb pour avoir la clé api)

```ts
export const environment = {
  tmdbApikey: 'VOTRE_CLÉ_TMDB_ICI'
};
```

### ▶️ Lancer le front

```bash
ng serve
```

Puis ouvrir dans ton navigateur :  
[http://localhost:4200](http://localhost:4200)

---

## 🔸 Back-End (.NET + MySQL)

### 🛠️ 1. Créer la base de données MySQL

Créer une base `nehmflix` et y importer les tables suivantes (SQL à adapter selon besoin) :

- Table `users`
- Table `media`
- Table `users_media`
- Enum `media_type` : (`film`, `serie`)
- Enum `liste_type` : (`regarde`, `a_regarder`)

> Tu peux créer le tout avec un fichier `nehmflix.sql` (non fourni ici).

### 🗂️ 2. Configuration de la chaîne de connexion

Dans `NehmFlix.API/appsettings.json`, mets :

```json
"ConnectionStrings": {
  "DefaultConnection": "server=localhost;port=3306;database=nehmflix;user=TON_UTILISATEUR;password=TON_MDP"
}
```

### 🚀 3. Lancer l’API

```bash
cd NehmFlix.API
dotnet run
```

L’API s’exécute ensuite sur :  
[http://localhost:5000](http://localhost:5000) ou [https://localhost:5001](https://localhost:5001)

---

## 🧩 Fonctionnalités

- ✅ Inscription / Connexion utilisateur avec mot de passe hashé
- 🔍 Recherche de films et séries avec TMDb
- ➕ Ajout d’un film ou d’une série dans la watchlist
- ❌ Suppression d’un média de la watchlist
- 📄 Détails du média : titre, note, résumé, année de sortie, etc.

---

## 📦 Technologies utilisées

| Outil / Lib           | Utilisation                    |
|------------------------|--------------------------------|
| Angular 19             | Front-end                      |
| .NET 8 (C#)            | API REST                       |
| MySQL                  | Base de données                |
| Bootstrap              | UI / Responsive design         |
| TMDb API               | Recherche de médias            |
| BCrypt.Net             | Sécurité (hash des mots de passe) |

---

## 📄 Licence

Projet réalisé dans un cadre académique.  
Les données sont issues de [The Movie Database (TMDb)](https://www.themoviedb.org/documentation/api).

---

## 👨‍💻 Développé par

**Nehemie** 
