using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class ReportGenerator {
    private string delimiter = ";";

    public void Savecsv() {
        string[] line;
        string filePath = @"./" + State.exerciseName + ".csv";

        if (!File.Exists(filePath)) {
            line = new string[]{"Exercise", "Arm", "Session Time", "Tries", "Correct Repetitions", "Correctness", "Average Time per Repetition", "Rest count", "Left Shoulder Up", "Right Shoulder Up", "Leaned Left", "Leaned Right"};
            File.WriteAllText(filePath, generateStringWithDelimiter(line));
        }

        String arm;

        if (State.isLeftArmSelected()) {
            arm = "Left";
        }
        else {
            arm = "Right";
        }

        int minutes = State.sessionTimeInt / 60;
        int seconds = State.sessionTimeInt % 60;
        

        String sessionTime = minutes.ToString("00") + ":" + seconds.ToString("00");

        if(State.correctReps != 0) {
            minutes = (State.sessionTimeInt / State.correctReps) / 60;
            seconds = (State.sessionTimeInt / State.correctReps) % 60;
        }
        else {
            minutes = seconds = 0;
        }
        

        String avgTimeRep = minutes.ToString("00") + ":" + seconds.ToString("00");


        line = new string[]{State.exerciseName, arm, sessionTime, "" + State.tries, "" + State.correctReps, "" + ((float)State.correctReps/State.tries)*100f, avgTimeRep, "" + State.restCount, "" + State.leftShoulderUp, "" + State.rightShoulderUp, "" + State.leaningLeft, "" + State.leaningLeft};
        File.AppendAllText(filePath, generateStringWithDelimiter(line));
    }

    private string generateStringWithDelimiter(string[] line) {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine(string.Join(delimiter, line));

        return sb.ToString();
    }
}
