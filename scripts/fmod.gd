extends Node

func _on_player_died():
	
	#FmodServer.play_one_shot("event:/test/test_2DAction_boing")
	var my_music_event = FmodServer.create_event_instance("event:/test/test_2DAction_boing")
	my_music_event.start()

func _ready():
	FmodServer.add_listener(0, self)
