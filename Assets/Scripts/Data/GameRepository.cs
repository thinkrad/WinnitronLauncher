﻿using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GameRepository : Singleton<GameRepository> {
	public List<Game> games;
	
	string GAMES_SUBDIRECTORY = "Games";

	string gamesDirectory;

	protected override void Awake() {
		base.Awake();
		gamesDirectory = Path.Combine(Application.dataPath, GAMES_SUBDIRECTORY);
		BuildList();
	}

	void BuildList() {
		var gamesDir = new DirectoryInfo(gamesDirectory);
		foreach (var gameDir in gamesDir.GetDirectories()) {
			games.Add(CreateRepresentation(gameDir));
		}
	}

	Game CreateRepresentation(DirectoryInfo gameDirectory) {
		//var metaInfoPath = Path.Combine(gameDirectory.FullName, gameDirectory.Name + ".txt");
		//var metaInfo = File.ReadAllLines(metaInfoPath);
		var name = gameDirectory.Name;//metaInfo[0];
		//var author = metaInfo[1];
		string author = null;
		var screenshot = new Texture2D(1024, 768);
		screenshot.LoadImage(File.ReadAllBytes(Path.Combine(gameDirectory.FullName, gameDirectory.Name + ".png")));
		var executablePath = Path.Combine(gameDirectory.FullName, gameDirectory.Name + ".exe");

		return new Game(name, author, screenshot, executablePath);
	}
}
