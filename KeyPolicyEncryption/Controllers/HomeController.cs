using KeyPolicyEncryption.Classes;
using KeyPolicyEncryption.Context;
using KeyPolicyEncryption.Models;
using KeyPolicyEncryption.Stream;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;


namespace KeyPolicyEncryption.Controllers
{
    public class HomeController : Controller
    {
        private static readonly string _securityParameter = "1@@#$)_!k_!)#*78!@#";
        private string _fileContent { get; set; }
        private DatabaseContext _database;
        private Stopwatch _timer = new Stopwatch();
        public HomeController(DatabaseContext database)
        {
            _database = database;
        }
        public IActionResult Index()
        {
            return View();
        }

        #region Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(Register registerData)
        {
            if (ModelState.IsValid)
            {
                //check if user already exists  

                var userExists = _database.User.Any(el => el.Username == registerData.Username);
                if (userExists)
                {
                    PrintMessage("Username Already Exists");
                    return View("Register", registerData);
                }

                /*
                 * if user does not exist,
                *get user employeetype, department and user role
                 */

                string employeeType = UserAttribute.GetEmployeeType(registerData.EmployeeType);
                string userDepartment = UserAttribute.GetDepartment(registerData.Department);
                string userRole = UserAttribute.GetUserRole(registerData.UserRole);

                //encrypt password
               string pass = Enc.EncryptPassword(registerData.Password);
                //

                //create user 
                User newUser = new User
                {
                    ID = registerData.ID,
                    Username = registerData.Username.ToUpper(),
                    Password = pass,
                    UserRole = userRole,
                    EmployeeType = employeeType,
                    Department = userDepartment,
                };

                //store user in the database
                _database.User.Add(newUser);
                _database.SaveChanges();

            }
            return View("Index");
        }
        #endregion

        #region Login
        [HttpGet]
        public IActionResult Login()
        {
            //Debugger.Break();
            return View();
        }

        [HttpPost]
        public IActionResult Login(User user)
        {
            //encrypt user password
           string userpass =  Enc.EncryptPassword(user.Password);

            user.Username = user.Username.ToUpper();
            /*
             *check if user is valid, 
             */
            bool isValid = _database.User.Any(el => el.Username == user.Username && el.Password == userpass);
            if (isValid)
            {
                /*
                 * if user is valid, get check if the valid user is an admin user
                 */
                 

                User validUser = _database.User.Where(u => u.Username == user.Username && u.Password == userpass).FirstOrDefault();


                //take user to the page that asks if he wants to encrypt or decrypt a file 
                ViewBag.UserID = validUser.ID;
                return View("EnterRequest");


            }

            ViewBag.message = "Wrong username or password, kindly try again";
            ViewBag.messageID = 1;
            return View();


        }
        #endregion

        #region EnterRequest
        [HttpGet]
        public IActionResult EnterRequest()
        {
            return View();
        }
        [HttpPost]
        public IActionResult EnterRequest(User userDetail)
        {

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        #endregion`

        #region ViewContent
        [HttpPost]
        public IActionResult ViewContent(EncryptionParameters encryptionParameters)
        {
            string Department = Request.Form["Department"];
            string EmployeeType = Request.Form["EmployeeType"];
            string UserRole = Request.Form["UserRole"];
            string path = encryptionParameters.filepath;
            _fileContent = ReadWrite.ReadData(path);
            int userid = Convert.ToInt32(Request.Form["UserID"]);

            ViewBag.fileContent = _fileContent;
            ViewBag.filepath = path;
            ViewBag.Department = Department;
            ViewBag.EmployeeType = EmployeeType;
            ViewBag.UserRole = UserRole;
            ViewBag.UserID = userid;

            return View("ChooseFile");
        }

        #endregion

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        #region Private Methods
        void PrintMessage(string Message)
        {
            ViewBag.Message = Message;
        }

        #endregion

        #region Encrypt
        [HttpGet]
        [Route("{userid}")]
        public IActionResult ChooseFile(int userid)
        {
            var user = _database.User.Where(el => el.ID == userid).SingleOrDefault();

            if (user.UserRole == "Dataowner")
            {
                ViewBag.Department = user.Department;
                ViewBag.EmployeeType = user.EmployeeType;
                ViewBag.UserRole = user.UserRole;
                ViewBag.UserID = userid;
                return View();
            }

            ViewBag.errMessage = "Your access level does not permit you to carry out this task, kindly reach out to your supervisor";
            ViewBag.errMessageID = 1;
            return View("EnterRequest");


        }

        [HttpPost]
        public IActionResult EncryptFile(EncryptionParameters encryptionParameters)
        {
            _timer.Reset();
            _timer.Start();
          
            Setup setup = new Setup(_securityParameter);
            string PK = setup.PublicKey();
            string MK = setup.MasterKey();

            int userid = Convert.ToInt32(Request.Form["UserID"]);

            User requestingUser = _database.User.Where(el => userid == el.ID).SingleOrDefault();

            List<string> userAttr = new List<string>
            {
                requestingUser.Department,
               requestingUser.EmployeeType,
               requestingUser.UserRole,
            };

            //get the user attributes and pass as an arguement to the encryption constructor

            Encryption encryption = new Encryption(PK, userAttr, encryptionParameters.fileContent);

            var ciphertext = encryption.getCypherText(PK, MK);
            _timer.Stop();
            ViewBag.time = _timer.Elapsed.TotalMilliseconds;
            ViewBag.PK = PK;
            ViewBag.MK = MK;
            //ViewBag.Mem = Convert.ToInt32(cpuCounter.NextValue()).ToString() + "%";  
            ViewBag.CipherText = ciphertext;
            ViewBag.filepath = Request.Form["filepath"];

            return View();
        }
        #endregion

        #region Decrypt 
        [HttpGet]
        [Route("decryptfile/{id}")]
        public IActionResult DecryptFile(int id)
        {
            ViewBag.UserID = id;
            return View();
        }

        [HttpPost]
        [Route("decryptfile/{id}")]
        public IActionResult Decrypt(EncryptionParameters encryptionParameters, int id)
        {
            if (ModelState.IsValid)
            {
                _timer.Reset();
                
                string path = encryptionParameters.filepath;
                string content = ReadWrite.ReadData(path);
                var splitContent = content.Split('.');

                List<string> escapeCharacters = new List<string>
                {
                    "\r",
                    "\n"
                };
                for (int i = 0; i < splitContent.Length; i++)
                {
                    foreach (string character in escapeCharacters)
                    {
                        if (splitContent[i].Contains(character))
                        {
                            splitContent[i] = splitContent[i].Replace(character, "");
                        }
                    }

                }

                int userid = id;
                _timer.Start();
                //get the decryption key
                KeyGen keyGen = new KeyGen(splitContent[splitContent.Length - 1], splitContent[0], splitContent[splitContent.Length - 2]);


                // get the primary key and master key from the encrypted context
                Decryption decryption = new Decryption(content, keyGen.DecryptionKey(), splitContent[0]);

                //get user attribute from database
                User requestingUser = _database.User.Where(el => userid == el.ID).SingleOrDefault();

                //add all user atributes to list 
                string decryptedMessage = decryption.getMessage(new List<string> { requestingUser.Department, requestingUser.EmployeeType, requestingUser.UserRole });

                _timer.Stop();

                ViewBag.fileContent = decryptedMessage;
                ViewBag.timer = _timer.Elapsed.TotalMilliseconds;

                if (decryptedMessage == "")
                {
                    ViewBag.fileContent = "You are not authorized to decrypt this file";
                    ViewBag.ID = 1;
                    return View("DecryptFile");
                }

                ViewBag.ID = 2;
                return View("DecryptFile");
            }
            return View();
        }
        #endregion
        
        

        #region SaveData
        [HttpPost]
        public IActionResult SaveData()
        {
            try
            {
                string content = Request.Form["cipher"];
                string path = Request.Form["filepath"];
                ReadWrite.WriteData(path, content);
                ViewBag.messageID = 1;
                ViewBag.Message = "Succesfully Saved";
                return View("EncryptFile");
            }
            catch (Exception ex)
            {
                ViewBag.messageID = 2;
                ViewBag.Message = ex.Message;
                return View("EncryptFile");
            }


        }
        #endregion
    }
}
