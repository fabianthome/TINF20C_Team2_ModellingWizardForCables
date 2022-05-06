# 1. Scope
The System Test Report details the tests run and their results. Eventual unit tests are contained in the Module definitions themselves. 

# 2. Definitions

**TC:** Test Case

**TS:** Test Suite

**TD:** Test Data

**AML:** Automation Markup Language

**CAEX:** Computer Aided Engineering Exchange (Dateityp)

**STR:** System Test Report

**STP:** System Test Plan


# 3. Test Objects

| Ref.-ID | Product Number | Product Name | 
|---------|----------------|--------------|
| 1       |  V 1.0         | Modelling Wizard for Cables |

# 4. Test Equipment

Any x86 computer with at least 16GB of RAM and 8 CPU cores with a symmetrical gigabit ethernet internet connection or greater. 
 
# 5. Results of Testsuite \<TS-001: Local installation\> 
## 5.1 Results of \<TC-001-001\> Access front end on local machine
|**Testcase ID:**| TC-001-001 |
|--|--|
|**Testcase Name:** | File Validation with valid input file |
|**Req.-ID:**| LF20, LF10, LF30, LF40, LF50, LF100|
|**Test Setup:** | The Balluff-BNI_IOL_355_S02_Z013-20170315-IODD1.1.xml is used as test file.|

**Test Steps:**

| Step | Action | Expected result | Actual Result |
|------|--------|-----------------|---------------|
| 1    | Execute the front end and back end and open the front end webpage at [localhost](http://localhost:4200/). | The front end cable display list is displayed. | 

**Tester:** Leon Amtmann

**Date:** 06-05-2022

**Testcase Result:** PASS

## 5.2  Results of \<TC-001-002\> (File Validation with invalid input file)

|**Testcase ID:**| TC-001-001 |
|--|--|
|**Testcase Name:** | File Validation with valid input file |
|**Req.-ID:**| LF20, LF80, LF100|
|**Test Setup:** | The BrokenBalluff-BNI_IOL_355_S02_Z013-20170315-IODD1.1.xml is used as test file.|

**Test Steps:**
|**Step**|**Action**|**Expected result**|**Actual result**|
|:-:|-|-|-|
|1|Install the DD2AML tool and open the CLI by typing cmd in the windows search.| The DD2AML tool is installed on the system. The CLI is open.|The DD2AML tool is installed on the system. The CLI is open.|
|2|Select a valid input file for the validation, for example: dd2aml –input /filePathTo/BrokenBalluff-BNI_IOL_355_S02_Z013-20170315-IODD1.1.xml -v 2|The conversion is aborted after the failed validation.|The conversion is terminated with an error message. |
|3|Then open the logs of the CLI. These can be found under: C:\Users\USERNAME\ AppData\Local\DD2AML\ Logs\CLI |After replacing the USERNAME tag with the real username, the CLI folder with all logs opens. The most recent log is opened.| After navigating to the CLI log folder, the latest log can be opened.|
|4|Look at the first error message in the logs.| The error message can be found approximately in the 5th line. Detailed information about the error, as well as line details are given.| The error message was found in the 5th line. In the test case it contains the following: The 'Feature' start tag on line 31 position 8 does not match the end tag of 'Features'. Line 33, position 9.|

**Tester:** Antonia Wermerskirch

**Date:** 07.05.2020

**Testcase Result:** PASS

# 6. Results of Testsuite Testsuite \<TS-002: Command Line Interface\>
## 6.1 Results of \<TC-002-001\> (View CLI help text)
|**Testcase ID:**| TC-002-001 |
|--|--|
|**Testcase Name:** | View CLI help text |
|**Req.-ID:**| LF60|
|**Test Setup:** | The Balluff-BNI_IOL_355_S02_Z013-20170315-IODD1.1.xml is used as test file.|

**Test Steps:**
|**Step**|**Action**|**Expected result**|**Actual result**|
|:-:|-|-|-|
|1|Install the DD2AML tool and open the CLI by typing cmd in the windows search.| The DD2AML tool is installed on the system. The CLI is open.|The DD2AML tool is installed on the system. The CLI is open.|
|2|Run the DD2AML CLI with valid arguments and use the help flag. |Regardless of the other valid arguments, only "--help" is executed and a help text is displayed, showing all possible functions.|As long as --help is included in the transfer parameters, the help text is displayed, which gives information about all possible transfer parameters.|

**Tester:** Antonia Wermerskirch

**Date:** 04.05.2020

**Testcase Result:** PASS

## 6.2 Results of \<TC-002-002\> (Converting without output flag)
|**Testcase ID:**| TC-002-002 |
|--|--|
|**Testcase Name:** |Converting without output flag|
|**Req.-ID:**| LF60|
|**Test Setup:** | The Balluff-BNI_IOL_355_S02_Z013-20170315-IODD1.1.xml is used as test file.|

**Test Steps:**
|**Step**|**Action**|**Expected result**|**Actual result**|
|:-:|-|-|-|
|1|Install the DD2AML tool and open the CLI by typing cmd in the windows search.| The DD2AML tool is installed on the system. The CLI is open.|The DD2AML tool is installed on the system. The CLI is open.|
|2|Run the DD2AML CLI with valid input flag, for example: dd2aml –input /filePathTo/Balluff-BNI_IOL_355_S02_Z013-20170315-IODD1.1.xml -v 2|The conversion is executed successfully. Since no output path is given, the output file is saved in the file path of the input file.|The output file will be saved in the input directory without further inquiry. If an output file with the same name already exists, the system will first ask whether it is allowed to overwrite it.|

**Tester:** Antonia Wermerskirch

**Date:** 04.05.2020

**Testcase Result:** PASS


# 7. Results of Testsuite \<TS-003: Graphical User Interface\>
## 7.1 Results of \<TC-003-001\> (GUI Input field verification)
|**Testcase ID:**| TC-003-001 |
|--|--|
|**Testcase Name:** | GUI Input field verification |
|**Req.-ID:**| LF70, LF90|
|**Test Setup:** | The DD2AML software should be downloaded and ready to run the setup for installation.|

**Test Steps:**
|**Step**|**Action**|**Expected result**|**Actual Result**|
|:-:|-|-|-|
|1|Install the DD2AML Software and open the GUI.|The software is installed and the GUI window opens.|With installed DD2AML Software the GUI window opens.|
|2|Try to start the conversion by pressing the “Convert” button at the bottom center.|Conversion not possible, because "Convert" button stays deactivated.|It is not possible to start the conversation by clicking "Convert" while the input field is empty.|

**Tester:** Antonia Wermerskirch

**Date:** 04.05.2020

**Testcase Result:** PASS
## 7.2 Results of \<TC-003-002\> (GUI Input file selection via file explorer)
|**Testcase ID:**| TC-003-002 |
|--|--|
|**Testcase Name:** | GUI Input file selection via file explorer |
|**Req.-ID:**| LF70, LF90|
|**Test Setup:** | The DD2AML software should be downloaded and ready to run the setup for installation.|

**Test Steps:**
|**Step**|**Action**|**Expected result**|**Actual Result**|
|:-:|-|-|-|
|1|Install the DD2AML Software and open the GUI.|The software is installed and the GUI window opens.|With installed DD2AML Software the GUI window opens.|
|2|Click on the "..." button at the end of the input text field.|CThe file explorer opens in a new window.|By clicking on "..." the file explorer opens.|
|3|Click on "Files" in the lower right corner directly above the buttons for open and cancel|A drop-down menu opens showing that only file suffix with .xml or .cspp are allowed.| The drop-down list shows that only files with .xml or .cspp as suffix are allowed. To be exact, it shows: Files (*.xml;*.cspp)|

**Tester:** Antonia Wermerskirch

**Date:** 04.05.2020

**Testcase Result:** PASS
## 7.3 Results of \<TC-003-003\> (GUI Input file selection via drag and drop)
|**Testcase ID:**| TC-003-003 |
|--|--|
|**Testcase Name:** | GUI Input file selection via drag and drop |
|**Req.-ID:**| LF70, LF90|
|**Test Setup:** | The DD2AML software should be downloaded and ready to run the setup for installation.|

**Test Steps:**
|**Step**|**Action**|**Expected result**|**Actual Result**|
|:-:|-|-|-|
|1|Install the DD2AML Software and open the GUI.|The software is installed and the GUI window opens.|With installed DD2AML Software the GUI window opens.|
|2|Open the file explorer and select any file. Drag the selected file and drop it into the GUI input text field.| If the selected file has a valid file suffix, its absolute file path will appear in the input field. If it has an invalid suffix, it is not possible to drop the file into the input field.| If a suffix is allowed, the file can be placed in the input field, then the absolute file path of the file is in the input field. For each invalid suffix, the mouse pointer changes to a no-drop symbol, when the file is dragged into the input field and it is not possible to drop the file.|

**Tester:** Antonia Wermerskirch

**Date:** 04.05.2020

**Testcase Result:** PASS
## 7.4 Results of \<TC-003-004\> (GUI Output file path generation)
|**Testcase ID:**| TC-003-004 |
|--|--|
|**Testcase Name:** | GUI Output file path generation |
|**Req.-ID:**| LF70, LF90|
|**Test Setup:** | The DD2AML software should be downloaded and ready to run the setup for installation. Example files from GSD, IODD and CSP+ must be available.|

**Test Steps:**
|**Step**|**Action**|**Expected result**|**Actual Result**|
|:-:|-|-|-|
|1|Install the DD2AML Software and open the GUI.|The software is installed and the GUI window opens.|With installed DD2AML Software the GUI window opens.|
|2|Select a valid file of IODD, CSP+ or GSD format in the Input text box.| As soon as the file including file path is in the input field, an output file is suggested for the same directory. The output file has the suffix .amlx and does not have the file format of the input file in its name.|For an IODD file, "-IODD1.1" is removed from the file name. For a CSP+ file, only the extension .cspp is replaced by .amlx. With the GSD file "GSD-" is removed from the name. The output file with the file path of the input file is displayed in the output text field.|

**Tester:** Antonia Wermerskirch

**Date:** 04.05.2020

**Testcase Result:** PASS

# 8. References