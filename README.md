<h1>Cancun Hotel API</h1>

<h3>API is developed for reservations management of one room in Cancun Hotel.</h3>
<h3>API provides functionality for creation, update and canceling reservations, also there is a method for checking room availability.</h3>
<h3>Please consider that all data will be stored in memory and it will be lost after service shut down.</h3>

<hr/>
<h3>Create booking</h3>
Reservation could be created via <i>POST</i> web method by following url:
<i>http://localhost:port/booking/book</i>

Payload example:
<i>{
  "bookedBy": "some name",
  "start": "30-12-2022",
  "end": "30-12-2022"
}</i>

Please pay attention that date format should be: <i>"dd-MM-yyyy"</i>
<hr/>

<h3>Update booking</h3>
Change reservation could be done via <i>PUT</i> web method by following url:
<i>http://localhost:port/booking/change</i>

Payload example:
<i>{
  "id": "guid" 
  "bookedBy": "some name",
  "start": "30-12-2022",
  "end": "30-12-2022"
}</i>

Guid will be generated after booking creation.

<hr/>

<h3>Cancel booking</h3>
Cancelation could be done via <i>DELETE</i> web method by following url:
<i>http://localhost:port/booking/cancel/{id}</i>

<hr/>

<hr/>

<h3>Chech awailability</h3>
Checking of available dates could be done via <i>GET</i> web method by following url:
<i>http://localhost:port/booking/available/{date}</i>
Response will be a list of available dates since <i>date</i> (input parameter) + 30 days since current (today's) date.
<hr/>

