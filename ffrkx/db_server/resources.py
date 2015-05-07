import mysql.connector
import sys

from ffrkx.util import log

class Cursor():
    def __init__(self, cursor):
        self.cursor = cursor

    def __enter__(self):
        pass

    def __exit__(self, type, value, traceback):
        try:
            self.cursor.close()
        except Exception as err:
            log.log_exception("An error occurred closing the cursor.")
            pass

class Transaction():
    def __init__(self, connection):
        self.connection = connection

    def __enter__(self):
        if self.connection == None:
            return
        try:
            self.connection.start_transaction()
        except mysql.connector.Error as err:
            log.log_exception("An error occurred starting the transaction.")
            raise

    def __exit__(self, type, value, traceback):
        if self.connection == None:
            return

        if (type == None) and (value == None) and (traceback == None):
            # If we're exiting with no exception, commit the transaction
            try:
                self.connection.commit()
            except mysql.connector.Error as err:
                log.log_exception("An error occurred comitting the data.")
        else:
            # If we're exiting with an exception, roll back the transaction
            try:
                self.connection.rollback()
            except mysql.connector.Error as err:
                log.log_exception("An error occurred rolling back the transaction.")
