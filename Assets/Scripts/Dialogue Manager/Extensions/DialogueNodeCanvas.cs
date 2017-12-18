using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeEditorFramework;

[NodeCanvasType("Dialogue Canvas")]
public class DialogueNodeCanvas : NodeCanvas
{
    public override string canvasName { get { return Name; } }
    public string Name = "Dialogue";
}
