
# Tags

Generic tagging and enrichment microservices for any item that relay in any system.


## Tag Definition

| Tag                | Url                                          | Retention |
| ------------------ | -------------------------------------------- | --------- |
| corporate-customer | cb.burgan.com.tr/corporate-customer/{param1} | 1m        |
| loan-partner       | cb.burgan.com.tr/partner/{param1}            | 1h        |
| burgan-bank        | cb.burgan.com.tr/bank-info                   | 1h        |
| retail-customer    | cb.burgan.com.tr/retail-customer/{param1}    | 1m        |
| potential-customer | cb.burgan.com.tr/application/{param1}        | 1m        |
| bank-staff         | cb.burgan.com.tr/staff/{param1}              | 1m        |
| customer-360       | cb.burgan.com.tr/360/{param1}                | 1m        |


## Templates

| Tag                | Type | Template              |
| ------------------ | ---- | --------------------- |
| corporate-customer | html | ui-corporate-customer |
| loan-partner       | html | ui-partner-info       |
| burgan-bank        | html | ui-burgan-splash      |
| retail-customer    | html | ui-retail-customer    |
| potential-customer | plain-text | ui-retail-customer    |
| bank-staff         | html | ui-staff-info         |
| customer-360       | html | ui-user-360           |
