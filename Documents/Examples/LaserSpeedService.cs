using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using AdvancedInspector;

public class LaserSpeedService : PhysicalProductAnimator {
	[Expandable(false)] public Transform plateLaser;
	[Expandable(false)] public Transform HouseCircuit;
	[Expandable(false)] public Transform LaserGenerate;
	[Expandable(false)] public PhysicalPart bolts;
	[Expandable(false)] public PhysicalPart inputModule;
	[Expandable(false)] public PhysicalPart lens;
	[Expandable(false)] public PhysicalPart plate;


	protected override void Start() {
		base.Start();
		basicMovePosition = .03f;
		 
		//top layer
		physicalProduct.justMovedToThisLayer += delegate (PhysicalSceneObject THIS, bool down) {
			//Debug.Log("PhysicialProduct is the new current layer. Animate accordingly.  " + (down ? "Came Down" : "Went Up"));
			if (down) {
				resetProduct();
				//start positions
				lens.transform.localScale = plate.transform.localScale = HouseCircuit.localScale = LaserGenerate.localScale = Vector3.one;
				lens.transform.localPosition = lens.originalPosition.position;
				bolts.gameObject.SetActive(true);
				bolts.transform.localPosition = bolts.originalPosition.position;
				plateLaser.localPosition = new Vector3(0,.00375f,-.0169f);
				HouseCircuit.localPosition = Vector3.zero;

			}
			else {//back from laser or circuit
				bolts.gameObject.SetActive(true);
				bolts.transform.DOLocalMoveZ(-.0094f, .25f).SetEase(Ease.InOutCubic).OnComplete(() => {  });
				plateLaser.DOLocalMoveY(.075f, .5f).SetEase(Ease.InCubic).Play();
				plateLaser.DOLocalMoveY(0.00375f, .5f).SetEase(Ease.OutCubic).SetDelay(.5f).Play();
				plateLaser.DOLocalRotate(new Vector3(-360, 0, 0), .75f, RotateMode.FastBeyond360).SetEase(Ease.InOutCubic).Play();
				plateLaser.DOLocalMoveZ(-.03f, .75f).SetEase(Ease.InOutCubic).Play();
				plateLaser.DOLocalMoveZ(-.0169f, .25f).SetEase(Ease.InOutCubic).SetDelay(.75f).Play();
			}
		};
		//laser system
		physicalProduct.parts[1].justMovedToThisLayer += delegate (PhysicalSceneObject THIS, bool down) {
			//Debug.Log("PhysicialProduct is the new current layer. Animate accordingly.  " + (down ? "Came Down" : "Went Up"));
			if (down) {
				bolts.transform.DOLocalMoveZ(-.04f, .25f).SetEase(Ease.InOutCubic).OnComplete(() => { bolts.gameObject.SetActive(false); });
				plateLaser.DOLocalMoveZ(-.03f, .25f).SetEase(Ease.InOutCubic).Play();
				plateLaser.DOLocalMoveZ(.03f, .75f).SetEase(Ease.InOutCubic).SetDelay(.25f).Play();
				plateLaser.DOLocalRotate(new Vector3(360, 0, 0), .75f, RotateMode.FastBeyond360).SetEase(Ease.InOutCubic).SetDelay(.25f).Play();
				plateLaser.DOLocalMoveY(.075f, .5f).SetEase(Ease.InCubic).SetDelay(.25f).Play();
				plateLaser.DOLocalMoveY(0, .5f).SetEase(Ease.OutCubic).SetDelay(.75f).Play(); 
			}
			else {
				LaserGenerate.DOScale(1, .5f).SetEase(Ease.InOutCubic);
				HouseCircuit.DOScale(1, .5f).SetEase(Ease.InOutCubic);
				plate.transform.DOScale(1, .5f).SetEase(Ease.InOutCubic);
				lens.transform.DOScale(1, .5f).SetEase(Ease.InOutCubic);
				lens.transform.DOLocalMove(new Vector3(-.01097f,-.1f,-.0394f), .5f).SetEase(Ease.InOutCubic);
			}
		};
		//laser system - Lens
		physicalProduct.parts[1].parts[0].justMovedToThisLayer += delegate (PhysicalSceneObject THIS, bool down) {
			//Debug.Log("PhysicialProduct is the new current layer. Animate accordingly.  " + (down ? "Came Down" : "Went Up"));
			if (down) {
				HouseCircuit.DOScale(0,.5f).SetEase(Ease.InOutCubic);
				LaserGenerate.DOScale(0, .5f).SetEase(Ease.InOutCubic);
				plate.transform.DOScale(0, .5f).SetEase(Ease.InOutCubic);
				lens.transform.DOScale(3, .5f).SetEase(Ease.InOutCubic);
				lens.transform.DOMove(new Vector3(0, -0.15f, 2.5f), .5f).SetEase(Ease.InOutCubic);
			} 
		};
		//circuit
		physicalProduct.parts[3].justMovedToThisLayer += delegate (PhysicalSceneObject THIS, bool down) {
			//Debug.Log("PhysicialProduct is the new current layer. Animate accordingly.  " + (down ? "Came Down" : "Went Up"));
			if (down) {
				bolts.transform.DOLocalMoveZ(-.04f, .25f).SetEase(Ease.InOutCubic).OnComplete(() => { bolts.gameObject.SetActive(false); });
				plateLaser.DOLocalMoveZ(-.03f, .25f).SetEase(Ease.InOutCubic).Play();
				plateLaser.DOLocalMoveZ(.03f, .75f).SetEase(Ease.InOutCubic).SetDelay(.25f).Play();
				plateLaser.DOLocalRotate(new Vector3(360, 0, 0), .75f, RotateMode.FastBeyond360).SetEase(Ease.InOutCubic).SetDelay(.25f).Play();
				plateLaser.DOLocalMoveY(.075f, .5f).SetEase(Ease.InCubic).SetDelay(.25f).Play();
				plateLaser.DOLocalMoveY(0, .5f).SetEase(Ease.OutCubic).SetDelay(.75f).Play();
			} 
		}; 

	}
	public void removePlateAndSystem() { steps.Add(new Step(new PhysicalPart[] { physicalProduct.parts[0], physicalProduct.parts[1] }, plateLaser.DOLocalMoveZ(-.05f, 1).SetEase(Ease.InOutCubic))); }
	public void removeHousingAndCircuits() {  steps.Add(new Step(new PhysicalPart[] { physicalProduct.parts[2], physicalProduct.parts[3] }, HouseCircuit.DOLocalMoveZ(.03f, 1).SetEase(Ease.InOutCubic))); }
	public void removeBolts() { steps.Add(new Step(bolts, genTween(bolts, AxisConstraint.Z, -.04f, 1))); }
	public void removeInputModule() { steps.Add(new Step(inputModule, new Tween[] { genTween(inputModule, AxisConstraint.X, .025f, .5f), genTween(inputModule, AxisConstraint.Z, .025f, .5f) }, new Vector3(0, 50, 0))); } 
}
