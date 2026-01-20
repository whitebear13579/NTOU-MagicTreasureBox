const express = require('express');
const router = express.Router();
const axios = require('axios');
const cheerio = require('cheerio');

const url = 'https://www.ntou.edu.tw/post/%E5%AD%B8%E6%A0%A1%E5%85%AC%E5%91%8A';

/* GET home page. */
router.get('/news', async function (req, res, next) {

  let allNews = [];
  await axios.get(url)
    .then(response => {
      const html = response.data;
      const $ = cheerio.load(html);
      //console.log(html);

      // Assuming the news items are within a specific HTML element, update the selector accordingly
      const newsItems = $('ul[role=tabpanel] li');

      newsItems.each((index, element) => {
        let news = {};
        //console.log(index);

        // Extract relevant information from each news item
        const title = $(element).find('a').attr('title').trim();
        const link = $(element).find('a').attr('href');
        const dateString = $(element).find('.tabpanel_date').text().trim();
        const publisher = dateString.substring(0, dateString.indexOf(" - "));
        const date = dateString.substring(dateString.indexOf(" - ") + 3);

        news['title'] = title;
        news['link'] = link;
        news['publisher'] = publisher;
        news['date'] = date;

        allNews.push(news);

      });
    })
    .catch(error => {
      console.error(`Error fetching page: ${error.message}`);
    });

  res.status(200).json(allNews);
});

module.exports = router;