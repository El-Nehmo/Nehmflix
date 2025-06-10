CREATE TYPE media_type AS ENUM ('film', 'serie');

CREATE TYPE liste_type AS ENUM ('regarde', 'a_regarder');

CREATE TABLE users (
    id SERIAL PRIMARY KEY,
    nom VARCHAR(100) NOT NULL,
    email VARCHAR(150) NOT NULL UNIQUE,
    mot_de_passe VARCHAR(255) NOT NULL,
    date_inscription TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE media (
    id_tmdb INT PRIMARY KEY,
    titre VARCHAR(255) NOT NULL,
    type media_type NOT NULL,
    annee_sortie VARCHAR(10),
    note DECIMAL(3,1),
    affiche_url TEXT,
    resume TEXT,
    dernier_update TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    genres TEXT
);

CREATE TABLE users_media (
    id SERIAL PRIMARY KEY,
    utilisateur_id INT NOT NULL REFERENCES users(id) ON DELETE CASCADE,
    media_id INT NOT NULL REFERENCES media(id_tmdb) ON DELETE CASCADE,
    type_liste liste_type NOT NULL,
    date_ajout TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);
