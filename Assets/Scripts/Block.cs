using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour {
	public Blocks type{
		get{
			return _type;
		} set {
			_type = value;

			SpriteRenderer sprite = this.GetComponent<SpriteRenderer>();

			switch(_type){
			case Blocks.MoonRocks:
				sprite.color = Color.white;
				break;
			case Blocks.Spice:
				sprite.color = Color.red;
				break;
			case Blocks.Unobtainium:
				sprite.color = Color.grey;
				break;
			case Blocks.OxyAle:
				sprite.color = Color.cyan;
				break;
			case Blocks.VesperGas:
				sprite.color = Color.yellow;
				break;
			case Blocks.TheFifthElement:
				sprite.color = Color.blue;
				break;
			}
		}
	}
	private Blocks _type = Blocks.MoonRocks;
}
