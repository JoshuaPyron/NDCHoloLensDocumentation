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
	[Expandable(false)] public PhysicalPart laserSystem;
	[Expandable(false)] public PhysicalPart circuit;
	[Expandable(false)] public PhysicalPart backPlate;
	[Expandable(false)] public PhysicalPart housing;

	protected override void Start() {
		base.Start();
		basicMovePosition = .03f;
		 
		//top layer
		physicalProduct.animateToThisLayer += delegate (PhysicalSceneObject THIS, bool down) { 
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
				bolts.transform.DOLocalMoveZ(-.0094f, .25f).SetEase(Ease.InOutCubic);
				plateLaser.DOLocalMoveY(.075f, .5f).SetEase(Ease.InCubic).Play();
				plateLaser.DOLocalMoveY(0.00375f, .5f).SetEase(Ease.OutCubic).SetDelay(.5f).Play();
				plateLaser.DOLocalRotate(new Vector3(-360, 0, 0), .75f, RotateMode.FastBeyond360).SetEase(Ease.InOutCubic).Play();
				plateLaser.DOLocalMoveZ(-.03f, .75f).SetEase(Ease.InOutCubic).Play();
				plateLaser.DOLocalMoveZ(-.0169f, .25f).SetEase(Ease.InOutCubic).SetDelay(.75f).Play();
			}
		};
		physicalProduct.jumpToThisLayer += delegate (PhysicalSceneObject THIS, bool down) { 
				resetProduct(); 
				lens.transform.localScale = plate.transform.localScale = HouseCircuit.localScale = LaserGenerate.localScale = Vector3.one;
				lens.transform.localPosition = lens.originalPosition.position;
				bolts.gameObject.SetActive(true);
				bolts.transform.localPosition = bolts.originalPosition.position;
				plateLaser.localPosition = new Vector3(0, .00375f, -.0169f);
				HouseCircuit.localPosition = Vector3.zero;  
		};

		//laser system
		laserSystem.animateToThisLayer += delegate (PhysicalSceneObject THIS, bool down) { 
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
				lens.transform.DOLocalMove(lens.originalPosition.position, .5f).SetEase(Ease.InOutCubic);
			}
		};
		laserSystem.jumpToThisLayer+= delegate (PhysicalSceneObject THIS, bool down) {
			if (down) {
				bolts.gameObject.SetActive(false);
				bolts.transform.localPosition = new Vector3(bolts.transform.localPosition.x, bolts.transform.localPosition.y, -.04f);
				plateLaser.localPosition = new Vector3(plateLaser.localPosition.x, 0, 0.03f); 
			}
			else {
				LaserGenerate.transform.localScale = HouseCircuit.transform.localScale = plate.transform.localScale = lens.transform.localScale = Vector3.one; 
				lens.transform.localPosition = lens.originalPosition.position;
			}
		};

		//laser system - Lens
		lens.animateToThisLayer += delegate (PhysicalSceneObject THIS, bool down) { 
			if (down) {
				HouseCircuit.DOScale(0,.5f).SetEase(Ease.InOutCubic);
				LaserGenerate.DOScale(0, .5f).SetEase(Ease.InOutCubic);
				plate.transform.DOScale(0, .5f).SetEase(Ease.InOutCubic);
				THIS.transform.DOScale(3, .5f).SetEase(Ease.InOutCubic);
				centerObject(THIS, new Vector3(0.02f, -0.08f, 0)); 
			} 
		};
		lens.jumpToThisLayer+= delegate (PhysicalSceneObject THIS, bool down) {
			if (down) {
				LaserGenerate.transform.localScale = HouseCircuit.transform.localScale = plate.transform.localScale = Vector3.zero;
				THIS.transform.localScale = new Vector3(3, 3, 3); 
				centerObject(THIS, new Vector3(0.02f, -0.08f, 0), 0, false);
			}
		};

		//circuit
		circuit.animateToThisLayer += delegate (PhysicalSceneObject THIS, bool down) { 
			if (down) {
				bolts.transform.DOLocalMoveZ(-.04f, .25f).SetEase(Ease.InOutCubic).OnComplete(() => { bolts.gameObject.SetActive(false); });
				plateLaser.DOLocalMoveZ(-.03f, .25f).SetEase(Ease.InOutCubic).Play();
				plateLaser.DOLocalMoveZ(.03f, .75f).SetEase(Ease.InOutCubic).SetDelay(.25f).Play();
				plateLaser.DOLocalRotate(new Vector3(360, 0, 0), .75f, RotateMode.FastBeyond360).SetEase(Ease.InOutCubic).SetDelay(.25f).Play();
				plateLaser.DOLocalMoveY(.075f, .5f).SetEase(Ease.InCubic).SetDelay(.25f).Play();
				plateLaser.DOLocalMoveY(0, .5f).SetEase(Ease.OutCubic).SetDelay(.75f).Play();
			} 
		}; 
		circuit.jumpToThisLayer += delegate (PhysicalSceneObject THIS, bool down) {
			if (down) {
				bolts.gameObject.SetActive(false);
				bolts.transform.localPosition = new Vector3(bolts.transform.localPosition.x, bolts.transform.localPosition.y, -.04f);
				plateLaser.localPosition = new Vector3(plateLaser.localPosition.x, 0, 0.03f);
			}
		};

	}
	public void removePlateAndSystem() {
		Step s = new Step(new PhysicalPart[] { backPlate, laserSystem }, new Tween[]{
			backPlate.transform.DOLocalMoveZ(-.05f, 1).SetEase(Ease.InOutCubic),
			laserSystem.transform.DOLocalMoveZ(-.05f, 1).SetEase(Ease.InOutCubic),
		});
		s.simultaneous = true;
		steps.Add(s);
	}
	public void removeHousingAndCircuits() {
		Step s = new Step(new PhysicalPart[] { housing, circuit }, new Tween[] {
			housing.transform.DOLocalMoveY(.03f, 1).SetEase(Ease.InOutCubic),
			circuit.transform.DOLocalMoveY(.0317f, 1).SetEase(Ease.InOutCubic)
		});
		s.simultaneous = true;
		steps.Add(s);
	}
	public void removeBolts() { steps.Add(new Step(bolts, genTween(bolts, AxisConstraint.Z, -.04f, 1))); }
	public void removeInputModule() { steps.Add(new Step(inputModule, new Tween[] { genTween(inputModule, AxisConstraint.X, .025f, .5f), genTween(inputModule, AxisConstraint.Z, .025f, .5f) }, new Vector3(0, 50, 0))); } 
}
