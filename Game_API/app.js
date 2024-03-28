"use strict";

import express from "express";
import mysql from "mysql2/promise"

const app = express();

const port = 5000;

app.use(express.json());

async function connectToDB() {
  return await mysql.createConnection({
    host: "localhost",
    user: "root",
    password: "diego",
    database: "time_clash_chronicles",
  });
}

app.get("/api/cards", async (request, response) => {
  let connection = null;

  try {

    connection = await connectToDB();

    const [results, fields] = await connection.execute("select * from card");

    response.status(200).json(results);
  }
  catch (error) {
    response.status(500);
    response.json(error);
    console.log(error);
  }
  finally {

    if (connection !== null) {
      connection.end();
      console.log("Connection closed succesfully!");
    }
  }
});

app.get("/api/cards/:id", async (request, response) => {
  let connection = null;

  try {
    connection = await connectToDB();

    const [results, fields] = await connection.execute("SELECT * FROM card WHERE card_id = ?", [request.params.id]);

    response.status(200).json(results);
  }
  catch (error) {
    response.status(500);
    response.json(error);
    console.log(error);
  }
  finally {

    if (connection !== null) {
      connection.end();
      console.log("Connection closed succesfully!");
    }
  }
});

app.post("/api/players", async (request, response) => {
  let connection = null;

  try {
    connection = await connectToDB();

    const playerData = request.body;

    const [results, fields] = await connection.execute(
      "INSERT INTO player (name) VALUES (?)",
      [playerData.name]
    );

    console.log(`${results.affectedRows} rows affected`);

    response.status(200).json({ message: "Jugador creado exitosamente" });
  } catch (error) {
    console.error(error);
    response.status(500).json({ error: "Error al crear el jugador" });
  } finally {
    if (connection !== null) {
      connection.end();
      console.log("Conexión cerrada exitosamente");
    }
  }
});

app.get("/api/players/:id", async (request, response) => {
  let connection = null;

  try {
    connection = await connectToDB();

    const [results, fields] = await connection.execute("SELECT * FROM player WHERE player_id = ?", [request.params.id]);

    response.status(200).json(results);
  }
  catch (error) {
    response.status(500);
    response.json(error);
    console.log(error);
  }
  finally {

    if (connection !== null) {
      connection.end();
      console.log("Connection closed succesfully!");
    }
  }
});


app.get("/api/players/deck/:id", async (request, response) => {
  let connection = null;

  try {
    connection = await connectToDB();

    const [results, fields] = await connection.execute("SELECT * FROM deck WHERE deck_id = ?", [request.params.id]);

    response.status(200).json(results);
  }
  catch (error) {
    response.status(500);
    response.json(error);
    console.log(error);
  }
  finally {

    if (connection !== null) {
      connection.end();
      console.log("Connection closed succesfully!");
    }
  }
});

app.post("/api/games", async (request, response) => {
  let connection = null;

  try {
    connection = await connectToDB();

    const data = request.body instanceof Array ? request.body : [request.body];

    for (const card of data) {

      const [results, fields] = await connection.execute(
        "INSERT INTO game (player1_id, player2_id, winner_id, arena_id, duration) VALUES (?,?,?,?,?)",
        [card.player1_id, card.player2_id, card.winner_id, card.arena_id, card.duration]
      );
      console.log(`${results.affectedRows} Game affected`);
      console.log(results);
    }

    response.status(200).json({ message: "Game added successfully" });
  }
  catch (error) {
    response.status(500);
    response.json(error);
    console.log(error);
  }
  finally {
    if (connection !== null) {
      connection.end();
      console.log("Connection closed succesfully!");
    }
  }
});


app.post("/api/turns", async (request, response) => {
  let connection = null;

  try {
    connection = await connectToDB();

    const turnData = request.body;

    // Assuming your 'turn' table has fields like 'player_id', 'game_id', 'turn_number', etc.
    const [results, fields] = await connection.execute(
      "INSERT INTO turn (player_id, game_id, turn_number, phase) VALUES (?,?,?,?)",
      [turnData.player_id, turnData.game_id, turnData.turn_number, turnData.phase]
    );

    console.log(`${results.affectedRows} rows affected`);

    response.status(200).json({ message: "Turno creado exitosamente" });
  } catch (error) {
    console.error(error);
    response.status(500).json({ error: "Error al crear el turno" });
  } finally {
    if (connection !== null) {
      connection.end();
      console.log("Conexión cerrada exitosamente");
    }
  }
});








app.listen(port, () => {
  console.log(`Escuchando en el puerto ${port}`);
});