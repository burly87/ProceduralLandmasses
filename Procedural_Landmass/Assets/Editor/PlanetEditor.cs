using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Planet))]
public class PlanetEditor : Editor
{
    Planet planet;
    Editor shapeEditor;
    Editor colorEditor;


    public override void OnInspectorGUI()
    {
        using (var check = new EditorGUI.ChangeCheckScope())
        {

            base.OnInspectorGUI();
            if (check.changed)
            {
                planet.GernatePlanet();
            }
        }
        
        // Button to generate a Planet
        if(GUILayout.Button("Generate Planet"))
        {
            planet.GernatePlanet();
        }

        DrawSettingsEditor(planet.shapeSettings, planet.OnShapeSettingsUpdated, ref planet.shapeSettingsFoldout, ref shapeEditor);
        DrawSettingsEditor(planet.colorSettings, planet.OnColorSettingsUpdated, ref planet.colorSettingsFoldout, ref colorEditor);
    }

    //foldout makes sure you can fold in and out the settings in Inspector GUI
    void DrawSettingsEditor(Object settings, System.Action onSettingsUpdated, ref bool foldout, ref Editor editor)
    {

        if (settings != null)
        {
            foldout = EditorGUILayout.InspectorTitlebar(foldout, settings);
            // check if something is changed
            using (var check = new EditorGUI.ChangeCheckScope())
            {

                if (foldout)
                {
                    CreateCachedEditor(settings, null, ref editor); // so we create an editor only if we have to 
                    editor.OnInspectorGUI();

                    if (check.changed)
                    {
                        if (onSettingsUpdated != null)
                        {
                            onSettingsUpdated();
                        }
                    }
                }
            }
        }

    }

    private void OnEnable()
    {
        planet = (Planet)target;
    }
}
