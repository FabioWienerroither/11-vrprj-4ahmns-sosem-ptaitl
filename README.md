# 11-vrprj-4ahmns-sosem-ptaitl

Dieses VR-Spiel ist im Rahmen des MTIN Unterrichts bei Frau Prof. Meerwald-Stadler, Dipl.-Ing. Susanne der HTL Salzburg entstanden. (Schuhljahr 2021/22)

## Aufgabenstellung

Auf Basis eines vorab bereitgestellten Unity / XR-Interaction-Toolkit Boilerplate soll eine spielbare .apk eines Oster VR-Games erstellt werden. Inhaltlich soll der Spieler durch das Lösen zweier Minigames aus einem vordefinierten Labyrinth enkommen.


> **Note:** Aufgrund vieler ausgefallender Unterrichtseinheiten wurden die Anforderungen auf ein Minigame inkl. Onboarding und Verabschiedung beschränkt und das ursprüngliche Konzept entsprechend angepasst (siehe unterhalb).

## Konzept
Vor dem Labyrinth wartet ein Hase. Kommt der Spieler in die Nähe des Hasen, erscheint über ihm eine Sprechblase mit Spielanweisungen. Um Buttoninputs zu vermeiden, indiziert ein UI-Slider wie lange die Sprechblase noch sichtbar ist.

Der Hase hoppelt zum Rätsel. Jedes Mal, wenn er den Boden berührt, hört der Spieler einen dumpfen Schlag, der ihn akustisch durch das Labyrinth leiten soll. Außerdem gibt der Hase hasentypische Idle- Sounds von sich. So gelingt es dem Spieler dem Hasen bis zum Rätsel zu folgen, obwohl der Hase unter Umständen schneller ist und der Spieler ihn auf dem Weg zum Rätsel verliert.

Sobald der Spieler das Labyrinth betritt, schließt sich der Eingang. Das Labyrinth kann dann nur mehr über den Ausgang verlassen werden. Beim Rätsel angekommen erscheint wieder eine Sprechblase über dem Hasen, in dem der Spieler aufgefordert wird ein mathematisches Rätsel zu lösen und das Ergebnis in ein Zahlenfeld einzugeben.

Hat der Spieler das Ergebnis über das Zahlenfeld eingegeben wird dieses grün und es erscheint wieder eine Sprechblase über dem Hasen. Dem Spieler wird gesagt, er könne jetzt den Ausgang suchen, um aus dem Labyrinth zu entkommen. Der Hase hoppelt zum Ausgang.

Der Spieler folgt dem Hasen bis zum Ausgang. Die Tür geht auf und der Hase verabschiedet sich.


## Stil & Spielspaß

Grundsätzlich soll das Spiel mithilfe eines Toonshaders (Tutorial von Erik Roystan), stilisierten Figuren und einem Natursetting eine helle und farbenfrohe Atmosphäre ausstrahlen. Für eine bessere Immersivität soll auch Wind, in Form von vorbeiwehenden Blättern und entsprechenden Soundeffekten sorgen. Der Spaßfaktor besteht also nicht nur daraus, seinen Kopf zu benutzten, um aus dem Labyrinth zu entkommen, sondern auch darin, die stilisierte Umgebung und interessante Klangkulisse auf sich wirken zu lassen.

## Auditive Gestaltung

Um das Spiel noch immersiver zu machen, sollen 3D Sounds, sowie 2D Atmo Sounds eingebaut werden. Folgende AudioClips sind im Projekt enthalten:

* Hase
  * Idle Sound
  * Hoppelsound, wenn er beim Springen den Biden berührt
* Minigame
  * Klick Sound, wenn beim ersten Rätsel ein Input getätigt wird (1x)
  * Bestätigungssound, wenn das Ergebnis stimmt (1x)
  * Fehlersound, wenn das Ergebnis nicht stimmt (1x)
* Atmo bzw. globaler Sound
  * Vögelgeräusche
  * Hintergrundmusik

## Arbeitsschritte
1. Projekt aufsetzten
2. Projekt builden und Fortbewegung, Scale, etc. am Headset ausprobieren
3. evtl. Bugs fixen
4. Modells suchen bzw. modellieren
5. Hasen und Führung durchs Spiel einbauen
6. Projekt builden und Führung ausprobieren
7. evtl. Bugs fixen
8. Rechenrätsel einbauen
9. Projekt builden und Rätsel ausprobieren
10. Restlichen Spielfluss einbauen (Sprechblasen)
11. Projekt builden und ausprobieren
12. evtl. Bugs fixen

Die letzten Schritte sind iterativ: Es alle Bugs gefixed, bis die .apk ohne Fehler und flüssig läuft.

## Limitations

Das Spiel ist lediglich auf Oculus VR-Brillen spielbar.

## Packages

- 2D Sprite 1.0.0
- JetBrains Rider Editor 2.0.7
- Oculus XR Plugin 1.10.0
- Post Processing 3.1.1
- Test Framework 1.1.29
- TextMeshPro 3.0.6
- Timeline 1.4.8
- Unity UI 1.0.0
- Version Control 1.9.0
- Visual Studio Code Editor 1.2.4
- Visual Studio Editor 2.0.11
- XR Interaction Toolkit 1.0.0-pre.8
- XR Plugin Management 4.2.0

> **Note:** Alle verwendeten Packages sind dauerhaft kostenlos und im Repository inkludiert. Beim Clonen des Projekts müssen keine zusätzlichen Packages installiert werden.

## Assets

Alle Assets sind kostenlose Modelle von folgenden Seiten:

- https://www.turbosquid.com
- https://www.cgtrader.com/

## Abschließende Anmerkung

Da es sich um ein Schulprojekt handelt, sind alle Scripts sehr detalliert kommentiert, damit das Beurteilen der Arbeit leichter fällt und Zusammenhänger besser verständlich werden.

Copyright © 2022 by ptaitl
