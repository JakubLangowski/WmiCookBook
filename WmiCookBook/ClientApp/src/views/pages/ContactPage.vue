<template>
  <div class="container">

    <div class="header">
      <div class="overlay">
        <div class="bg-image">
          <div class="aaa">KONTAKT</div>
        </div>
      </div>


      <div class="content">
        <div class="a111">
          Skontaktuj się z nami
        </div>

        <div class="sub-header">
          Jeżeli masz jakieś uwagi, znalazłeś błąd w przepisie lub chcesz zapytać o coś administrację - napisz do nas.
        </div>


        <div class="flex flex-wrap">

          <div class="contact-form">
            <input type="text" placeholder="Imię"><br>
            <input type="text" placeholder="Nazwisko"><br>
            <input type="text" placeholder="E-mail"><br>
            <textarea placeholder="Wiadomość"></textarea>
            <button>Wyślij</button>
          </div>

          <div class="google-maps">
            <iframe
                src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d2430.6594346850934!2d16.92497051620924!3d52.46719497980428!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x47044325a4f6681b%3A0x5ec2f670808f7183!2sWydzia%C5%82%20Matematyki%20i%20Informatyki%20UAM!5e0!3m2!1spl!2spl!4v1612902398044!5m2!1spl!2spl"
                height="500" frameborder="0" style="border:0;" allowfullscreen="" aria-hidden="false"
                tabindex="0"></iframe>
          </div>

        </div>


      </div>

    </div>


  </div>
</template>

<script>
export default {
  name: "ContactPage",
}
</script>

<style scoped>
.container {
  width: 100%;
  height: 100%;
}

.bg-image {
  background-image: url('../../assets/images/background.jpg');
  opacity: 0.3;
  height: 400px;
  background-size: cover;
}

.overlay {
  background-color: #1F2937;
}

.a111 {
  text-align: center;
  margin-left: auto;
  margin-right: auto;
  font-size: 36px;
  font-weight: 700;
  padding-top: 40px;
}

.sub-header {
  text-align: center;
  margin-left: auto;
  margin-right: auto;
  font-size: 16px;
  padding-bottom: 40px;

}

.contact-form {
  text-align: center;
  flex: 1 1 50%;
  padding-bottom: 40px;
}

.google-maps {
  text-align: center;
  flex: 1 1 50%;
  padding-bottom: 40px;

}

.google-maps iframe {
  width: 100%;
  min-width: 250px;
}

input {
  width: 90%;
  height: 35px;
  margin-bottom: 10px;
  padding: 10px;
  font-size: 16px;
  border-width: 1px;
  border-color: #CCCCCC;
  background-color: #FFFFFF;
  color: #000000;
  border-style: solid;
  border-radius: 5px;
  box-shadow: 0px 0px 4px rgba(66, 66, 66, .75);
  text-shadow: 0px 0px 0px rgba(66, 66, 66, .75);
}

textarea {
  width: 90%;
  height: 200px;
  margin-bottom: 10px;
  padding: 10px;
  font-size: 16px;
  border-width: 1px;
  border-color: #CCCCCC;
  background-color: #FFFFFF;
  color: #000000;
  border-style: solid;
  border-radius: 5px;
  box-shadow: 0px 0px 4px rgba(66, 66, 66, .75);
  text-shadow: 0px 0px 0px rgba(66, 66, 66, .75);
}

button {
  background-color: #1f2937;
  border-radius: 28px;
  border: 1px solid #1f2937;
  display: inline-block;
  cursor: pointer;
  color: #ffffff;
  font-family: Arial;
  font-size: 17px;
  padding: 16px 36px;
  text-decoration: none;
  text-shadow: 0px 1px 0px #000000;
  width: 95%;
}

button:hover {
  background-color: #2c3b4f;
}

.content {
}

.aaa {
  text-align: center;
  color: black;

  padding-top: 150px;
  font-size: 55px;
  font-weight: 700;

  text-shadow: 0px 4px 2px #CECECE;
}
</style>