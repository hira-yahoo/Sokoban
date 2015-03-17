﻿using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {
	public GameObject PlayerCharacter;
	public GameObject MovableCube;
	public GameObject UnmovableCube;
	public GameObject Destination;
	public Canvas cvs;

	private static Game instance;

	private Stage stage;
	public static int currentStage = 1;

	void Start () {
		instance = this;
		UnityEngine.UI.Text t = getStageLabel ();
		t.text = "Stage " + currentStage;

		this.stage = new Stage(
			"uuuuuuuuuu" +
			"uuuuuuuuuu" +
			"uuuuuuuuuu" +
			"uuu pdd  u" +
			"uuu  mm  u" +
			"uuu     uu" +
			"uuuuuuuuuu" +
			"uuuuuuuuuu" +
			"uuuuuuuuuu" +
			"uuuuuuuuuu"
			);
		
		View view = new View ();
		for (int x = 0; x < Stage.WIDTH; x++) {
			for (int z = 0; z < Stage.HEIGHT; z++) {
				int c = getCell(x, z);
				if (c == Stage.UNMOVABLE) {
					view.instantiateObject(UnmovableCube, x, z);
				}
				else if (c == Stage.MOVABLE) {
					GameObject obj = view.instantiateObject(MovableCube, x, z);
					MovableCube movable = obj.GetComponent<MovableCube>();
					movable.create(cvs);
				}
				else if (c == Stage.DESTINATION) {
					view.instantiateObject(Destination, x, z);
				}
				else if (c == Stage.PLAYER_CHARACTER) {
					view.instantiateObject(PlayerCharacter, x, z);
				}

			}
		}
	}

	private UnityEngine.UI.Text getStageLabel() {
		for (int i = 0; i < cvs.transform.childCount; i++) {
			Transform child = cvs.transform.GetChild(i);
			if (child.gameObject.name == "CurrentStage") {
				return child.gameObject.GetComponent<UnityEngine.UI.Text>();
			}
		}
		throw new UnityException ();
	}



	public static Game getInstance() {
		return instance;
	}

	public int getCell(int x, int z) {
		return stage.getCell (x, z);
	}

	public bool isMovableAt(Coordinate position) {
		int cell = getCell(position.x, position.z);
		switch (cell) {
		case Stage.NONE:
		case Stage.DESTINATION:
			return true;
		}

		return false;
	}

	public bool moveCube(Coordinate current, Coordinate next) {
		return stage.moveCube(current, next);
	}

	public void retryGame(int i) {
		if (i == 0) {
			Application.LoadLevel( Application.loadedLevel );
		}
	}


}
