using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace FromUriTryOut.APIs {

    public class Person
	{
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
	}

    public class PersonController : ApiController {

        //POST /api/person?Name=Foo&Surname=Bar&Age=17
        public Person Post([FromUri]Person person) {

            return person;
        }
    }
}